namespace POS_System.Common;

public class ErrorDetails
{
    public string Title { get; set; } = "An error has occurred.";
    public int Status { get; set; }
    public string? Details { get; set; }
    public string? ParameterName { get; set; }
}