using Swagify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swagify
{
    class SignupPageModel : BaseViewModel
    {
        string name, email, password, verifyPassword, errorMessage;
        HttpSignupRequest signupRequest = new HttpSignupRequest(App.HttpClient);
        HttpLoginRequest httpLoginRequest = new HttpLoginRequest(App.HttpClient);


        public SignupPageModel(INavigation navigation, Page page) : base(navigation, page)
        {
            this.GoToLoginCommand = new Command(async () => await navigation.PushAsync(new LoginPage()));
            this.SignupCommand = new Command(() => Signup());
            ErrorMessage = "";
        }

        private bool ValidateEmailAndPass()
        {
            Regex validEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if(string.IsNullOrWhiteSpace(name))
            {
                ErrorMessage = "Enter your name.";
                return false;
            } else if (string.IsNullOrWhiteSpace(email) || !validEmailRegex.IsMatch(email))
            {
                ErrorMessage = "Invalid email.";
                return false;
            } else if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 20)
            {
                ErrorMessage = "Password must be between 6 and 20 characters.";
                return false;
            } else if (password == password.ToLower() || password == password.ToUpper() || !password.Any(char.IsDigit))
            {
                ErrorMessage = "Password must contain at least one lower case, upper case, and number.";
                return false;
            } else if (password != verifyPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return false;
            }
            return true;
        }

        public async void Signup()
        {
            if (ValidateEmailAndPass())
            {
                var signupSuccess = await signupRequest.PostAccountCreation(name, email, password);

                if (signupSuccess)
                {
                    ErrorMessage = "";
                    var loginSuccess = await httpLoginRequest.GetLoginUser(email, password);

                    if (loginSuccess)
                    {
                        Navigation.InsertPageBefore(new MainPage(), Page);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await Navigation.PushAsync(new LoginPage());
                    }
                }
                else
                {
                    ErrorMessage = "Email already in use.";
                } 
            }
        }

        public ICommand GoToLoginCommand { protected set; get; }
        public ICommand SignupCommand { protected set; get; }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
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

        public string VerifyPassword
        {
            get { return verifyPassword; }
            set
            {
                if (verifyPassword != value)
                {
                    verifyPassword = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if(errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }
    }
}
