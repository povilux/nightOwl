using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nightOwl
{
    public static class DataStorage
    {
        public static Image<Bgr,Byte> picFromWebcam { get; set; }
    }
}
