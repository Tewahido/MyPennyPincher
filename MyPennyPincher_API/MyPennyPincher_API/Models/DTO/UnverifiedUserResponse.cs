using System.ComponentModel.DataAnnotations;

namespace MyPennyPincher_API.Models.DTO;

public class UnverifiedUserResponse
{
    [EmailAddress]
    public string Email { get; set; }

    public bool IsVerified { get; set; }
}
