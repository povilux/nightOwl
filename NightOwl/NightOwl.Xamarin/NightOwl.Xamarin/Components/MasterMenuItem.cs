using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NightOwl.Xamarin.Components
{
    public class MasterMenuItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }

        public MasterMenuItem(string Title, string IconSource, Type target)
        {
            this.Title = Title;
            this.IconSource = IconSource;
            TargetType = target;
        }
    }
}
