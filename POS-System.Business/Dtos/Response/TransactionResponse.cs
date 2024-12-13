using POS_System.Common.Enums;

namespace POS_System.Business.Dtos.Response
{
    public record TransactionResponse(
        DateTime Date,
        int Amount,
        int? Tip,
        string TransactionRef,
        TransactionStatusEnum Status
    );
}