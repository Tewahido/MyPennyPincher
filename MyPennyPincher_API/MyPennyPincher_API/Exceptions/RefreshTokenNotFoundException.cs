namespace MyPennyPincher_API.Exceptions
{
    public class RefreshTokenNotFoundException : Exception
    {
        public RefreshTokenNotFoundException(string message): base(message) { }
    }
}
