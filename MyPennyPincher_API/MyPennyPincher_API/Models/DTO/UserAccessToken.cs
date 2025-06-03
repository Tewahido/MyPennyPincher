namespace MyPennyPincher_API.Models.DTO;

public class UserAccessToken
{
    public Guid UserId { get; set; }

    public string Token { get; set; } = "";
}
