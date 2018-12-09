using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NightOwl.Xamarin.Services
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();  
    }
}
