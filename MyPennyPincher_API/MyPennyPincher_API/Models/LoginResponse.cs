namespace MyPennyPincher_API.Models;

public class LoginResponse
{
    public Guid UserId { get; set; }

    public string Token { get; set; } = null;
}
