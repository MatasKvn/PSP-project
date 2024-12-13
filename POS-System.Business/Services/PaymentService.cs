using AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;
using POS_System.Business.Services.Interfaces;
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
        ICartDiscountService cartDiscountService,
        IMapper mapper,
        ICartService cartService,
        IUnitOfWork unitOfWork
    ) : IPaymentService
    {
        public async Task<CheckoutResponse> FullCheckoutAsync(CheckoutRequest checkoutRequest, string referer, CancellationToken token)
        {   
            var cartTask = cartService.GetByIdAsync(checkoutRequest.CartId, token);

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

            var discount = new SessionDiscountOptions { Coupon = (await cartTask).DiscountId };

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
                CancelUrl = $"{referer}failed",
                PaymentMethodTypes = [ "card" ],
                Mode = "payment",
                LineItems = lineItems
            };

            var cart = await cartTask;
            if (cart.DiscountId is not null)
                options.Discounts = [ new SessionDiscountOptions { Coupon = cart.DiscountId } ];

            var session = await sessionService.CreateAsync(options);

            var transaction = new Transaction
            {
                Id = transactionDate,
                Amount = (ulong) session.AmountTotal!,
                Tip = checkoutRequest.Tip,
                TransactionRef = session.Id,
                Status = TransactionStatusEnum.PENDING,
                CartId = checkoutRequest.CartId
            };

            await unitOfWork.TransactionRepository.CreateAsync(transaction);
            await unitOfWork.SaveChangesAsync();

            return new CheckoutResponse(session.Id, configuration["Stripe:PublicKey"]!); 
        }

        public async Task<PartialCheckoutResponse> InitializePartialCheckoutAsync(InitPartialCheckoutRequest checkoutRequest, CancellationToken token)
        {
            var totalPrice = checkoutRequest.CartItems.Sum(item => item.Price);
            var cart = await cartService.GetByIdAsync(checkoutRequest.CartId, token);

            if (cart.DiscountId is not null)
            {
                var couponService = new CouponService();
                var discount = await couponService.GetAsync(cart.DiscountId, cancellationToken: token);

                if (discount.Valid)
                {
                    totalPrice -= discount.PercentOff is null ? (int)discount.AmountOff! : (int)(totalPrice * ((double)discount.PercentOff!) * 0.01);

                    if (totalPrice < 0)
                        totalPrice = 0;
                }
            }

            if (totalPrice < NumericConstants.MINIMUM_AMOUNT_PARTIAL_PAYMENT)
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
            }

            await unitOfWork.TransactionRepository.CreateRangeAsync(transactions);
            cart.Status = CartStatusEnum.PENDING;
            await unitOfWork.SaveChangesAsync();

            return new PartialCheckoutResponse(mapper.Map<List<TransactionResponse>>(transactions));
        }

        public async Task<CheckoutResponse> PartialCheckoutAsync(PartialCheckoutRequest checkoutRequest, CancellationToken token)
        {
            var transactions = await unitOfWork.TransactionRepository.GetAllByExpressionAsync(t => t.CartId == checkoutRequest.CartId && t.Status != TransactionStatusEnum.SUCCEEDED);

            if (transactions.Count == 0)
                throw new BadRequestException(ApplicationMessages.CLOSED_ORDER);

            var sessionService = new SessionService();
            var server = serviceProvider.GetRequiredService<IServer>();
            var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

            var apiUrl = serverAddressesFeature!.Addresses.FirstOrDefault()
                ?? throw new InternalServerErrorException(ApplicationMessages.WEB_HOST_NOT_SET);

            var transactionToExec = transactions.First();
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{apiUrl}/api/payments/partial-checkout-success?transactionDate={transactionToExec.Id:MM/dd/yyyy HH:mm:ss.fffffff}&cartId={checkoutRequest.CartId}&sessionId=" + "{CHECKOUT_SESSION_ID}",
                CancelUrl = $"{apiUrl}/failed", // pridet failed endpointa
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

            var session = await sessionService.CreateAsync(options);
            
            return new CheckoutResponse(session.Id, configuration["Stripe:PublicKey"]!); 
        }

        public async Task<string> FullCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId)
        {
            var sessionService = new SessionService();

            var session = await sessionService.GetAsync(sessionId);
            var cartStatus = CartStatusEnum.IN_PROGRESS;
            var transactionStatus = TransactionStatusEnum.FAILED;

            if (session.PaymentStatus == "paid")
            {
                cartStatus = CartStatusEnum.COMPLETED;
                transactionStatus = TransactionStatusEnum.SUCCEEDED;
            }

            await Task.WhenAll(
                cartService.UpdateCartStatusAsync(cartId, cartStatus),
                UpdateTransactionStatus(transactionDate, transactionStatus));

            return "http://localhost:3000/dashboard/products/0";
        }

        public async Task<string> PartialCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId)
        {
            var transactionsTask = unitOfWork.TransactionRepository.GetAllByExpressionAsync(t => t.CartId == cartId && t.Status != TransactionStatusEnum.SUCCEEDED);
            var sessionService = new SessionService();

            var session = await sessionService.GetAsync(sessionId);
            var transactionStatus = session.PaymentStatus == "paid" ? TransactionStatusEnum.SUCCEEDED : TransactionStatusEnum.FAILED;

            var transactions = await transactionsTask;
            var completedTransaction = transactions.FirstOrDefault(t => t.Id == transactionDate);
            completedTransaction!.Status = transactionStatus;

            if (transactions.Count == 1 && transactionStatus == TransactionStatusEnum.SUCCEEDED)
                await cartService.UpdateCartStatusAsync(cartId, CartStatusEnum.COMPLETED);

            return "http://localhost:3000/dashboard/products/0";
        }

        private async Task UpdateTransactionStatus(DateTime transactionDate, TransactionStatusEnum status)
        {
            var transaction = await unitOfWork.TransactionRepository.GetByIdDateTimeAsync(transactionDate)
                ?? throw new NotFoundException(ApplicationMessages.NOT_FOUND_ERROR);

            transaction.Status = status;
            await unitOfWork.SaveChangesAsync();
        }
    }
}