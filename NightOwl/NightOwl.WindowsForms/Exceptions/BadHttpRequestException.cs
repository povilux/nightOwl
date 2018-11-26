using System;

namespace NightOwl.WindowsForms.Exceptions
{
    public class BadHttpRequestException : Exception
    {
        public BadHttpRequestException() : base() { }
        public BadHttpRequestException(string message) : base(message) { }

    }
}
