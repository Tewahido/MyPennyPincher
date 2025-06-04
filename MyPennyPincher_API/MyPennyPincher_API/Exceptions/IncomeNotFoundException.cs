namespace MyPennyPincher_API.Exceptions
{
    public class IncomeNotFoundException : Exception
    {
        public IncomeNotFoundException(int incomeId) : base($"Income with ID of {incomeId} not found.") { }
    }
}
