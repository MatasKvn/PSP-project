namespace POS_System.Business.Dtos.Request;

public record class GiftCardRequest
{
    public DateOnly Date { get; init; }
    public int Value { get; init; }
    public required string Code { get; init; }
}