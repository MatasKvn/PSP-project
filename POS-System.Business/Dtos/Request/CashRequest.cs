namespace POS_System.Business.Dtos.Request
{
    public record CashRequest(
        int CartId,
        ulong Amount,
        int? Tip,
        string TransactionRef
    );
}