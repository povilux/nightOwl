using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonRecognitionService.Components
{
    public class Trainer
    {
        // TO DO: move constants to config file
        public int NumOfComponents { get; set; } = 80;
        public int Threshold { get; set; } = 8000;
    }
}