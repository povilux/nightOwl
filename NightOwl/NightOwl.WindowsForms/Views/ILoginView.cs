﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.WindowsForms.Views
{
    public interface ILoginView
    {
        event EventHandler LoginUserClicked;
        string UserName { get; }
        string Password { get; set; }

        void Hide();
    }
}
