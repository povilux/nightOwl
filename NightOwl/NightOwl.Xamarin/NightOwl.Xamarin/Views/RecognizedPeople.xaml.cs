using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NightOwl.Xamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecognizedPeople : ContentPage
	{
		public RecognizedPeople ()
		{
			InitializeComponent ();
		}

        private void OnYesButtonClicked(object sender, EventArgs e)
        {

        }

        private void OnNoButtonClicked(object sender, EventArgs e)
        {

        }
	}
}