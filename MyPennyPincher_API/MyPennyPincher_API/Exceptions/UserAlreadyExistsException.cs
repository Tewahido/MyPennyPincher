﻿namespace MyPennyPincher_API.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) :base(message) { }
    }
}
