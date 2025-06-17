using System.ComponentModel.DataAnnotations;

namespace MyPennyPincher_API.Models.DataModels;

public partial class User
{
    [Key]
    public Guid UserId { get; set; } = new Guid();
    
    public string FullName { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public virtual ICollection<Expense>? Expenses { get; set; } = new List<Expense>();
    
    public virtual ICollection<Income>? Incomes { get; set; } = new List<Income>();
}
