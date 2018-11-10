using System;

namespace nightOwl.Exceptions
{
    public class BadHttpRequestException : Exception
    {
        public BadHttpRequestException() { }
        public BadHttpRequestException(string message) { }
    }
}
