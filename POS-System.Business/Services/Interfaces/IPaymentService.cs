using POS_System.Business.Dtos.Request;
using POS_System.Business.Dtos.Response;

namespace POS_System.Business.Services.Interfaces
{
    public interface IPaymentService
  	{
		Task<CheckoutResponse> FullCheckoutAsync(CheckoutRequest checkoutRequest, string referer, CancellationToken token);
		Task<PartialCheckoutResponse> InitializePartialCheckoutAsync(InitPartialCheckoutRequest checkoutRequest, CancellationToken token);
		Task<CheckoutResponse> PartialCheckoutAsync(PartialCheckoutRequest checkoutRequest, CancellationToken token);
		Task<string> FullCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId);
		Task<string> PartialCheckoutSuccessAsync(DateTime transactionDate, string sessionId, int cartId);
	}
}