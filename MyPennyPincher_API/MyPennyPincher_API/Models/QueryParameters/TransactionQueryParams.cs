namespace MyPennyPincher_API.Models.QueryParameters;

public class TransactionQueryParams
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}
