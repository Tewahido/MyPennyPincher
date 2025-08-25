using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Models.DTO;

public class IncomeResponse
{
    public List<Income> Data { get; set; }
    public int Count {  get; set; }
}
