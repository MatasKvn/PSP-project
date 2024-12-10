namespace POS_System.Business.Dtos.Response;

public record GiftCardResponse
{
    public int Id { get; init; }
    public DateOnly Date { get; init; }
    public int Value { get; init; }
    public required string Code { get; init; }
}
