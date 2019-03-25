using Swagify.APIs;
using Swagify.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swagify
{
    class LoginPageModel : BaseViewModel
    {
        string email, password, errorMessage;
        HttpLoginRequest httpLoginRequest = new HttpLoginRequest(App.HttpClient);

        public LoginPageModel(INavigation navigation, Page page) : base(navigation, page)
        {
            this.GoToSignupCommand = new Command(async () => await navigation.PushAsync(new SignupPage()));
            this.LogInCommand = new Command(() => LogIn());
            ErrorMessage = "";
        }

        public async void LogIn()
        {
            {
                bool success = await httpLoginRequest.GetLoginUser(email, password);
                if (success)
                {
                    ErrorMessage = "";
                    Navigation.InsertPageBefore(new TabbedPage1(), Page);
                    await Navigation.PopAsync();
                } else
                {
                    ErrorMessage = "Something went wrong. Try again later.";
                }
            }
        }

        public ICommand GoToSignupCommand {  protected set; get; }
        public ICommand LogInCommand { protected set; get; }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
            
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
    }
}
