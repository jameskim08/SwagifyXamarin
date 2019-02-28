using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Swagify
{
    public partial class App : Application
    {
        public static HttpClient HttpClient { get; private set; }
        public App()
        {
            InitializeComponent();
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("http://34.73.210.226/")
            };
            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
