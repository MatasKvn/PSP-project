namespace POS_System.Business.Dtos.Response
{
    public record PartialCheckoutResponse(List<TransactionResponse> Transactions);
}