using Swagify.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Swagify
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageModel(Navigation, this);

            emailEntry.Completed += (object sender, EventArgs e) =>
            {
                passwordEntry.Focus();
            };
        }
    }
}
