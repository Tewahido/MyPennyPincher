namespace MyPennyPincher_API.Exceptions
{
    public class PasswordTooWeakException : Exception
    {
        public PasswordTooWeakException() : base($"Password is too weak") { }

    }
}
