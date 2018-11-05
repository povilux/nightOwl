﻿using Emgu.CV;
using Emgu.CV.Structure;

namespace nightOwl.Components
{
    public struct Face
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public Image<Gray, byte> Image { get; set; }
    }
}