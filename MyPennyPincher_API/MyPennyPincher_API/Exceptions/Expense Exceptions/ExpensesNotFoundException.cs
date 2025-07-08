namespace MyPennyPincher_API.Exceptions.ExpenseExceptions
{
    public class ExpensesNotFoundException : Exception
    {
        public ExpensesNotFoundException(string message): base(message) { }
    }
}
