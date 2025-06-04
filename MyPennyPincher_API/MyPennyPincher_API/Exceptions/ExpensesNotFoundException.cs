namespace MyPennyPincher_API.Exceptions
{
    public class ExpensesNotFoundException : Exception
    {
        public ExpensesNotFoundException(string message): base(message) { }
    }
}
