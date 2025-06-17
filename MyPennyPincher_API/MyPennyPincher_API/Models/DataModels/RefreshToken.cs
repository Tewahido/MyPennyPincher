namespace MyPennyPincher_API.Models.DataModels;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public Guid UserId { get; set; }
}
