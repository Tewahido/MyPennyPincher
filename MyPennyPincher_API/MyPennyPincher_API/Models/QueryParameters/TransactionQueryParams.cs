namespace MyPennyPincher_API.Models.QueryParameters;

public class TransactionQueryParams
{
    public DateOnly PeriodStart { get; set; } = new DateOnly(QueryConstants.currentYear, QueryConstants.currentMonth, 1);
    public DateOnly PeriodEnd { get; set; } = new DateOnly(QueryConstants.currentYear, QueryConstants.currentMonth, QueryConstants.lastDayOfMonth);
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}

public class QueryConstants
{
    public readonly static int currentYear = DateTime.Now.Year;
    public readonly static int currentMonth = DateTime.Now.Month;
    public readonly static int lastDayOfMonth = DateTime.DaysInMonth(currentYear, currentMonth);
}
