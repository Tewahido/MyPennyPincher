namespace MyPennyPincher_API.Models.DTO;

public class LoginResponse
{
    public Guid UserId { get; set; }

    public string Token { get; set; } = null;
}
