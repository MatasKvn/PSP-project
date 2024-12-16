using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IPaymentService
  	{
		Task<TransactionResponse> RegisterCashTransactionAsync(CashRequest cashRequest, CancellationToken token);
		Task<List<TransactionResponse>> GetTransactionsByCartAsync(int cartId);
		Task<TransactionResponse> IssueRefundAsync(DateTime transactionId, RefundRequest refundRequest, CancellationToken token);
		Task<CheckoutResponse> FullCheckoutAsync(CheckoutRequest checkoutRequest, CancellationToken token);
		Task<PartialCheckoutResponse> InitializePartialCheckoutAsync(InitPartialCheckoutRequest checkoutRequest, CancellationToken token);
		Task<CheckoutResponse> PartialCheckoutAsync(PartialCheckoutRequest checkoutRequest, CancellationToken token);
		Task<string> FullCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId);
		Task<string> PartialCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId);
		Task<string> CheckoutFailAsync(DateTime transactionDate, string sessionId, int cartId);
	}
}