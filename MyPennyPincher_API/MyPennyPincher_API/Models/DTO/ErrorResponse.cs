namespace MyPennyPincher_API.Models.DTO;

public class ErrorResponse
{
    public int StatusCode { get; set; } = 1;
    public string Message { get; set; } = "";
}
