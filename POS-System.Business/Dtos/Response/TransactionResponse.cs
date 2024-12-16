using POS_System.Common.Enums;

namespace POS_System.Business.Dtos.Response
{
    public record TransactionResponse(
        DateTime Id,
        ulong Amount,
        int? Tip,
        string TransactionRef,
        TransactionStatusEnum Status
    );
}