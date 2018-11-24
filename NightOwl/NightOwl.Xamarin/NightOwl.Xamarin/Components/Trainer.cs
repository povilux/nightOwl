using System.Collections.Generic;

namespace NightOwl.Xamarin.Components
{
    public class Trainer
    {
        public IEnumerable<Face> Data { get; set; }
        public int NumOfComponents { get; set; }
        public int Threshold { get; set; }
    }
}
