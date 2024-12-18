using AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
using POS_System.Business.Utils;
using POS_System.Common.Constants;
using POS_System.Common.Enums;
using POS_System.Common.Exceptions;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;
using Stripe;
using Stripe.Checkout;

namespace POS_System.Business.Services
{
    public class PaymentService(
        IConfiguration configuration,
        IServiceProvider serviceProvider,
        IMapper mapper,
        ICartService cartService,
        IUnitOfWork unitOfWork,
        SmsService smsService
    ) : IPaymentService
    {
        public async Task<TransactionResponse> RegisterCashTransactionAsync(CashRequest cashRequest, CancellationToken token)
        {
            var transaction = mapper.Map<Transaction>(cashRequest);

            transaction.Id = DateTime.UtcNow;
            transaction.TransactionRef = $"CASH_{cashRequest.TransactionRef}";
            transaction.Status = TransactionStatusEnum.CASH;
            await unitOfWork.TransactionRepository.CreateAsync(transaction, token);
            await unitOfWork.SaveChangesAsync();

            var cartStatus = CartStatusEnum.COMPLETED;
            await cartService.UpdateCartStatusAsync(cashRequest.CartId, cartStatus, token);

            if (cashRequest.PhoneNumber is not null)
                await smsService.SendMessageAsync(cashRequest.PhoneNumber, cashRequest.CartId);

            return mapper.Map<TransactionResponse>(transaction);
        }

        public async Task<List<TransactionResponse>> GetTransactionsByCartAsync(int cartId)
        {
            var transactions = await unitOfWork.TransactionRepository.GetAllByExpressionAsync(t => t.CartId == cartId);

            return mapper.Map<List<TransactionResponse>>(transactions);
        }

        public async Task<TransactionResponse> IssueRefundAsync(DateTime transactionId, RefundRequest refundRequest, CancellationToken token)
        {
            var cart = await unitOfWork.CartRepository.GetByIdAsync(refundRequest.CartId, token)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            if (cart.Status != CartStatusEnum.COMPLETED)
                throw new BadRequestException(ApplicationMessages.INVALID_REFUND_REQUEST);

            var transactions = await unitOfWork.TransactionRepository.GetAllByExpressionAsync(t => t.CartId == refundRequest.CartId && (t.Status == TransactionStatusEnum.CASH || t.Status == TransactionStatusEnum.SUCCEEDED), token)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            var transaction = transactions.FirstOrDefault(t => t.Id == transactionId)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            if (refundRequest.IsCard)
            {   
                var refundService = new RefundService();
                var options = new RefundCreateOptions
                {
                    PaymentIntent = transaction.TransactionRef
                };
                var refund = await refundService.CreateAsync(options, cancellationToken: token);
                transaction.TransactionRef = refund.Id;
            }
            
            transaction.Status = TransactionStatusEnum.REFUNDED;

            if (transactions.Count == 1)
                cart.Status = CartStatusEnum.REFUNDED;
            
            await unitOfWork.SaveChangesAsync(token);

            return mapper.Map<TransactionResponse>(transaction);
        }

        public async Task<CheckoutResponse> FullCheckoutAsync(CheckoutRequest checkoutRequest, CancellationToken token)
        {   
            var cart = await unitOfWork.CartRepository.GetByIdAsync(checkoutRequest.CartId, token)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            if (cart.Status != CartStatusEnum.IN_PROGRESS)
                throw new BadRequestException(ApplicationMessages.CLOSED_ORDER);

            var lineItems = checkoutRequest.CartItems
                .Select(item => new SessionLineItemOptions
                {
                    PriceData = new()
                    {
                        UnitAmount = item.Price,
                        Currency = "EUR",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = item.Description,
                            Images = [ item.ImageURL ]
                        }
                    },
                    Quantity = item.Quantity
                })
                .ToList();

            var sessionService = new SessionService();
            var server = serviceProvider.GetRequiredService<IServer>();
            var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

            var apiUrl = serverAddressesFeature!.Addresses.FirstOrDefault()
                ?? throw new InternalServerErrorException(ApplicationMessages.WEB_HOST_NOT_SET);

            if (checkoutRequest.Tip is not null)
            {
                lineItems.Add(new SessionLineItemOptions() 
                {
                    PriceData = new()
                    {
                        UnitAmount = checkoutRequest.Tip,
                        Currency = "EUR",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Tip"
                        }
                    },
                    Quantity = 1
                });
            }

            var transactionDate = DateTime.UtcNow;
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{apiUrl}/api/payments/full-checkout-success?transactionDate={transactionDate:MM/dd/yyyy HH:mm:ss.fffffff}&cartId={checkoutRequest.CartId}&sessionId=" + "{CHECKOUT_SESSION_ID}",
                CancelUrl = $"{apiUrl}/api/payments/checkout-fail?transactionDate={transactionDate:MM/dd/yyyy HH:mm:ss.fffffff}&cartId={checkoutRequest.CartId}&sessionId=" + "{CHECKOUT_SESSION_ID}",
                PaymentMethodTypes = [ "card" ],
                Mode = "payment",
                LineItems = lineItems
            };

            if (cart.CartDiscountId is not null)
                options.Discounts = [ new SessionDiscountOptions { Coupon = cart.CartDiscountId } ];

            if (checkoutRequest.PhoneNumber is not null)
                options.SuccessUrl += $"&phoneNumber={checkoutRequest.PhoneNumber}";

            var session = await sessionService.CreateAsync(options, cancellationToken: token);

            var transaction = new Transaction
            {
                Id = transactionDate,
                Amount = (ulong) session.AmountTotal!,
                Tip = checkoutRequest.Tip,
                TransactionRef = session.Id,
                Status = TransactionStatusEnum.PENDING,
                CartId = checkoutRequest.CartId
            };

            await unitOfWork.TransactionRepository.CreateAsync(transaction, token);
            cart.Status = CartStatusEnum.PENDING;
            await unitOfWork.SaveChangesAsync(token);

            return new CheckoutResponse(session.Id, configuration["Stripe:PublicKey"]!); 
        }

        public async Task<PartialCheckoutResponse> InitializePartialCheckoutAsync(InitPartialCheckoutRequest checkoutRequest, CancellationToken token)
        {
            var totalPrice = checkoutRequest.CartItems.Sum(item => item.Price * item.Quantity);
            var cart = await cartService.GetByIdAsync(checkoutRequest.CartId, token);

            if (cart is null || cart.Status != CartStatusEnum.IN_PROGRESS)
                throw new BadRequestException(ApplicationMessages.CLOSED_ORDER);

            if (cart.CartDiscountId is not null)
            {
                var couponService = new CouponService();
                var discount = await couponService.GetAsync(cart.CartDiscountId, cancellationToken: token);

                if (discount.Valid)
                {
                    totalPrice -= discount.PercentOff is null ? (int)discount.AmountOff! : (int)(totalPrice * ((double)discount.PercentOff!) * 0.01);

                    if (totalPrice < 100)
                        totalPrice = 100;
                }
            }

            if (totalPrice < ApplicationConstants.MINIMUM_AMOUNT_PARTIAL_PAYMENT)
                throw new BadRequestException(ApplicationMessages.INSUFFICIENT_TOTAL_AMOUNT);

            var amount = totalPrice / checkoutRequest.PaymentCount;
            var transactions = new List<Transaction>();
            foreach (var i in Enumerable.Range(0, checkoutRequest.PaymentCount))
            {      
                if (i == checkoutRequest.PaymentCount - 1)
                {
                    amount = totalPrice - (amount * i);
                    if (checkoutRequest.Tip is not null)
                        amount += (int)checkoutRequest.Tip;
                }

                transactions.Add(new Transaction
                {
                    Id = DateTime.UtcNow,
                    Amount = (ulong)amount,
                    Tip = i == checkoutRequest.PaymentCount - 1 ? checkoutRequest.Tip : null,
                    TransactionRef = "",
                    Status = TransactionStatusEnum.PENDING,
                    CartId = checkoutRequest.CartId
                });
                await Task.Delay(1, token);
            }

            await cartService.UpdateCartStatusAsync(checkoutRequest.CartId, CartStatusEnum.PENDING, token);
            await unitOfWork.TransactionRepository.CreateRangeAsync(transactions);
            await unitOfWork.SaveChangesAsync(token);

            return new PartialCheckoutResponse(mapper.Map<List<TransactionResponse>>(transactions));
        }

        public async Task<CheckoutResponse> PartialCheckoutAsync(PartialCheckoutRequest checkoutRequest, CancellationToken token)
        {
            var transactionToExec = await unitOfWork.TransactionRepository.GetByIdDateTimeAsync(checkoutRequest.Id, token);

            if (transactionToExec is null || transactionToExec.Status != TransactionStatusEnum.PENDING)
                throw new BadRequestException(ApplicationMessages.CLOSED_ORDER);

            var sessionService = new SessionService();
            var server = serviceProvider.GetRequiredService<IServer>();
            var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

            var apiUrl = serverAddressesFeature!.Addresses.FirstOrDefault()
                ?? throw new InternalServerErrorException(ApplicationMessages.WEB_HOST_NOT_SET);

            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{apiUrl}/api/payments/partial-checkout-success?transactionDate={transactionToExec.Id:MM/dd/yyyy HH:mm:ss.fffffff}&cartId={checkoutRequest.CartId}&sessionId=" + "{CHECKOUT_SESSION_ID}",
                CancelUrl = $"{apiUrl}/api/payments/checkout-fail?transactionDate={transactionToExec.Id:MM/dd/yyyy HH:mm:ss.fffffff}&cartId={checkoutRequest.CartId}&sessionId=" + "{CHECKOUT_SESSION_ID}",
                PaymentMethodTypes = [ "card" ],
                Mode = "payment",
                LineItems = 
                [
                    new SessionLineItemOptions()
                    {
                        PriceData = new()
                        {
                            UnitAmount = (long)transactionToExec.Amount,
                            Currency = "EUR",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Partial payment for cart {checkoutRequest.CartId}",
                            }
                        },
                        Quantity = 1
                    }
                ]
            };
            
            if (checkoutRequest.GiftCard is not null)
            {
                var (giftCardCode, discount) = await CreateCouponForGiftCard((long)transactionToExec.Amount, checkoutRequest.GiftCard, token);
                
                options.Discounts = [ new SessionDiscountOptions { Coupon = giftCardCode }];
                options.CancelUrl += $"&giftCardCode={checkoutRequest.GiftCard.Code}&discount={discount}";
            }

            if (checkoutRequest.PhoneNumber is not null)
                options.SuccessUrl += $"&phoneNumber={checkoutRequest.PhoneNumber}";

            var session = await sessionService.CreateAsync(options, cancellationToken:token);
        
            transactionToExec.TransactionRef = session.Id;
            await unitOfWork.SaveChangesAsync(token);

            return new CheckoutResponse(session.Id, configuration["Stripe:PublicKey"]!); 
        }

        public async Task<string> FullCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId, string? phoneNumber)
        {
            var sessionService = new SessionService();

            var session = await sessionService.GetAsync(sessionId);
            var cartStatus = CartStatusEnum.IN_PROGRESS;
            var transactionStatus = TransactionStatusEnum.FAILED;

            if (session.PaymentStatus == "paid")
            {
                cartStatus = CartStatusEnum.COMPLETED;
                transactionStatus = TransactionStatusEnum.SUCCEEDED;

                if (phoneNumber is not null)
                    await smsService.SendMessageAsync(phoneNumber, cartId);
            }

            await UpdateTransactionStatus(transactionDate, session.PaymentIntentId, transactionStatus);
            await cartService.UpdateCartStatusAsync(cartId, cartStatus);

            return ApplicationConstants.REDIRECT_URL;
        }

        public async Task<string> PartialCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId, string? phoneNumber)
        {
            var transactionsTask = unitOfWork.TransactionRepository.GetAllByExpressionAsync(t => t.CartId == cartId && t.Status == TransactionStatusEnum.PENDING);
            var sessionService = new SessionService();

            var session = await sessionService.GetAsync(sessionId);
            var transactionStatus = session.PaymentStatus == "paid" ? TransactionStatusEnum.SUCCEEDED : TransactionStatusEnum.FAILED;

            var transactions = await transactionsTask;
            var completedTransaction = transactions.FirstOrDefault(t => t.Id == transactionDate);
            await UpdateTransactionStatus(transactionDate, session.PaymentIntentId, transactionStatus);

            if (transactions.Count == 1 && transactionStatus == TransactionStatusEnum.SUCCEEDED)
            {
                await cartService.UpdateCartStatusAsync(cartId, CartStatusEnum.COMPLETED);
                
                if (phoneNumber is not null)
                    await smsService.SendMessageAsync(phoneNumber, cartId);
            }
            else
            {
                await unitOfWork.SaveChangesAsync();
            }

            return ApplicationConstants.REDIRECT_URL;
        }

        public async Task<string> CheckoutFailAsync(DateTime transactionDate, string sessionId, int cartId, string? giftCardCode, long? discount)
        {
            var cart = await unitOfWork.CartRepository.GetByIdAsync(cartId, CancellationToken.None);
            var transaction = await unitOfWork.TransactionRepository.GetByIdDateTimeAsync(transactionDate);
            
            transaction!.TransactionRef = sessionId;
            transaction.Status = TransactionStatusEnum.PENDING;

            cart!.Status = CartStatusEnum.PENDING;
            
            if (giftCardCode is not null && discount is not null)
            {
                var giftCard = await unitOfWork.GiftCardRepository.GetByIdStringAsync(giftCardCode);

                if (giftCard is not null)
                    giftCard.Value += (long)discount;
            }

            await unitOfWork.SaveChangesAsync();

            return ApplicationConstants.REDIRECT_URL;
        }

        private async Task<(string, long)> CreateCouponForGiftCard(long totalPrice, GiftCardDetails giftCardDetails, CancellationToken token)
        {
            var couponService = new CouponService();

            var giftCard = await unitOfWork.GiftCardRepository.GetByIdStringAsync(giftCardDetails.Code, token);

            if (giftCard is null || giftCard.Value <= 0 || giftCard.Date < DateOnly.FromDateTime(DateTime.UtcNow))
                throw new NotFoundException(ApplicationMessages.GIFT_CARD_NOT_VALID);

            long toDeduct = giftCard.Value < giftCardDetails.ValueToSpend 
                ? giftCard.Value
                : giftCardDetails.ValueToSpend;
            long discount;

            if (toDeduct >= totalPrice)
            {
                giftCard.Value -= totalPrice - 100;
                discount = totalPrice - 100;
            }
            else
            {
                discount = toDeduct;
                giftCard.Value -= discount;
            }

            var options = new CouponCreateOptions
            {
                Currency = "EUR",
                AmountOff = discount,
                RedeemBy = DateTime.UtcNow.AddMinutes(5)
            };

            var coupon = await couponService.CreateAsync(options, cancellationToken: token)
                ?? throw new InternalServerErrorException(ApplicationMessages.INTERNAL_SERVER_ERROR); 

            await unitOfWork.SaveChangesAsync(token);   
            
            return (coupon.Id, discount);
        }

        private async Task UpdateTransactionStatus(DateTime transactionDate, string paymentId, TransactionStatusEnum status)
        {
            var transaction = await unitOfWork.TransactionRepository.GetByIdDateTimeAsync(transactionDate)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);
            
            transaction.TransactionRef = paymentId;
            transaction.Status = status;
            await unitOfWork.SaveChangesAsync();
        }
    }
}