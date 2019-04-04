using Swagify;
using Swagify.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swagify
{
    class LoginPageModel : BaseViewModel
    {
        string email, password, errorMessage;
        bool isLoading;
        HttpLoginRequest httpLoginRequest = new HttpLoginRequest(App.HttpClient);

        public LoginPageModel(INavigation navigation, Page page) : base(navigation, page)
        {
            IsLoading = false;
            this.GoToSignupCommand = new Command(async () => await navigation.PushAsync(new SignupPage()));
            this.LogInCommand = new Command(() => LogIn());
            this.OnFocusCommand = new Command(() => ClearErrorMessage());
            ErrorMessage = "";
        }

        public async void LogIn()
        {
            {
                if (!String.IsNullOrEmpty(Email) && !String.IsNullOrWhiteSpace(Email) && !String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(Password))
                {
                    IsLoading = true;
                    HttpStatusCode statusCode = await httpLoginRequest.GetLoginUser(email, password);
                    if (statusCode == HttpStatusCode.OK)
                    {
                        ErrorMessage = "";
                        Navigation.InsertPageBefore(new MainPage(), Page);
                        await Navigation.PopAsync();
                        IsLoading = false;
                    }
                    else if (statusCode == HttpStatusCode.Unauthorized)
                    {
                        IsLoading = false;
                        ErrorMessage = "Invalid email or password.";
                    }
                    else
                    {
                        IsLoading = false;
                        ErrorMessage = "Something went wrong. Please try again later.";
                    }
                }
                else
                {
                    ErrorMessage = "Please fill in your email and password.";
                }
            }
        }

        public void ClearErrorMessage()
        {
            ErrorMessage = "";
        }

        public ICommand GoToSignupCommand {  protected set; get; }
        public ICommand LogInCommand { protected set; get; }
        public ICommand OnFocusCommand { set; get; }

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged("IsLoading");
                }
            }
        }

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
