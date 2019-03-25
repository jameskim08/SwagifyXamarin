using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Swagify.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPage : ContentPage
	{
		public SignupPage ()
		{
			InitializeComponent();
            BindingContext = new SignupPageModel(Navigation, this);

            nameEntry.Completed += (object sender, EventArgs e) =>
            {
                emailEntry.Focus();
            };
            emailEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };
            passwordEntry.Completed += (object sender, EventArgs e) =>
            {
                verifyPasswordEntry.Focus();
            };
        }
	}
}