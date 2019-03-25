using Swagify.Models;
using System;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Swagify
{
    public partial class App : Application
    {
        private string oauthToken;
        public static HttpClient HttpClient { get; private set; }

        public App()
        {
            InitializeComponent();
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("http://34.73.210.226/api/")
            };

           

            if (string.IsNullOrEmpty(oauthToken))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new TabbedPage1());
            }

        }

        protected override async void OnStart()
        {
            try
            {
                oauthToken = await SecureStorage.GetAsync("oauth_token");
            }
            catch (Exception ex)
            {
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    oauthToken = (string) Current.Properties["token"];
                }
                Console.Write(ex.Message);
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
