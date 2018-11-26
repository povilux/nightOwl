using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace NightOwl.PersonRecognitionService.Components
{
    public class RecognitionData
    {
        public byte[] Photo { get; set; }
        public Rectangle FaceRectangle { get; set; }
    }
}