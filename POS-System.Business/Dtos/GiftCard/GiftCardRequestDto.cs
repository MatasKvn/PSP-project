namespace POS_System.Business.Dtos.GiftCard;

public record class GiftCardRequestDto
{
    public DateOnly Date { get; init; }
    public int Value { get; init; }
    public required string Code { get; init; }
}