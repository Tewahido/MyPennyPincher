namespace MyPennyPincher_API.Models.QueryParameters;

public class TransactionQueryParams
{
    public int Year { get; set; } = DateTime.Now.Year;
    public int Month { get; set; } = DateTime.Now.Month;
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}
