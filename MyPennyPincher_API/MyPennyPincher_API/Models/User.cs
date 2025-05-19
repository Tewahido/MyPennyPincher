using System.Text.Json.Serialization;

namespace MyPennyPincher_API.Models;

public partial class User
{
    public Guid UserId { get; set; } = new Guid();

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    public virtual ICollection<Expense>? Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Income>? Incomes { get; set; } = new List<Income>();
}
