using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Models.DTO;

public class ExpenseResponse
{
    public List<Expense> Data { get; set; }
    public int Count { get; set; }
}
