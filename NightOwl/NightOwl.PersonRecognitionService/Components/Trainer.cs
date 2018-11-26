using NightOwl.PersonRecognitionService.Models;
using System.Collections.Generic;

namespace NightOwl.PersonRecognitionService.Components
{
    public class Trainer
    {
        public IEnumerable<Face> Data { get; set; }
        public int NumOfComponents { get; set; }
        public int Threshold { get; set; }
    }
}