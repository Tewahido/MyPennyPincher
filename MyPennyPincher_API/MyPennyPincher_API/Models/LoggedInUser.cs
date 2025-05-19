namespace MyPennyPincher_API.Models;

public class LoggedInUser
{
    public Guid UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null;
    public DateTime ExpiresAt { get; set; }
}
