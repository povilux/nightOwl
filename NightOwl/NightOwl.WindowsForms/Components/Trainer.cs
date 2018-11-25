using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.WindowsForms.Components
{
    public class Trainer
    {
        public IEnumerable<Face3> Data { get; set; }
        public int NumOfComponents { get; set; }
        public int Threshold { get; set; }
    }
}
