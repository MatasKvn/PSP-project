namespace POS_System.Business.Dtos.Request;

public record GiftCardRequest
{
    public DateOnly Date { get; init; }
    public int Value { get; init; }
}