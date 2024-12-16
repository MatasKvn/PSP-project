namespace POS_System.Business.Dtos.Request
{
    public record RefundRequest(int CartId, bool IsCard);
}