using System;
using System.Collections.Generic;
using System.Text;

namespace NightOwl.Xamarin.Components
{
    public class APIMessage<T>
    {
        public bool Success { get; set; }
        public T Message { get; set; }
        public string Error { get; set; } = null;

        public APIMessage() {}
       
    }
}
