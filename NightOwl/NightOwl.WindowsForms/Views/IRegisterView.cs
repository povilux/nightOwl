using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.WindowsForms.Views
{
    public interface IRegisterView
    {
        event EventHandler UserRegistered;

        string Username { get; } 
        string Password { get; } 
        string Email { get; }

        void Hide();
    }
}
