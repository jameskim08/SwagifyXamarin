using Swagify.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Swagify
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                await Navigation.PushAsync(new TabbedPage1());
            }
        }

        private async void OnSignupButtonClicked(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                HttpSignupRequest signupRequest = new HttpSignupRequest(App.HttpClient);
                bool success = await signupRequest.PostAccountCreation(emailEntry.Text, passwordEntry.Text);

                if (success)
                {
                    await Navigation.PushAsync(new TabbedPage1());
                } else
                {
                    await DisplayAlert("Error", "Something went wrong with the signup.", "OK");
                }
            }
        }

        private bool ValidateInputs()
        {
            bool isValidEmail = ValidateEmail();
            bool isValidPassword = ValidatePassword();

            if (isValidEmail && isValidPassword)
            {
                emailErrorlabel.Text = "";
                passwordErrorlabel.Text = "";
                return true;
            }

            if (isValidEmail) emailErrorlabel.Text = "";
            if (isValidPassword) passwordErrorlabel.Text = "";

            return false;
        }

        private bool ValidateEmail()
        {
            String email = emailEntry.Text;
            Regex validEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrWhiteSpace(email) || !validEmailRegex.IsMatch(email))
            {
                emailErrorlabel.Text = "Invalid Email.";
                return false;

            }

            return true;
        }

        private bool ValidatePassword()
        {
            String password = passwordEntry.Text;
            if (password.Length < 4 || password.Length > 20)
            {
                passwordErrorlabel.Text = "Password must be between 4 and 20 characters.";
                return false;
            }

            if (password == password.ToLower() || password == password.ToUpper() || !password.Any(char.IsDigit))
            {
                passwordErrorlabel.Text = "Password must contain at least one lower case, upper case, and number.";
                return false;
            }

            return true;
        }
    }
}
