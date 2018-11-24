using System;

namespace NightOwl.Xamarin.Exceptions
{
    public class BadHttpRequestException : Exception
    {
        public BadHttpRequestException() : base() { }
        public BadHttpRequestException(string message) : base(message) { }

    }
}
