using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Swagify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageModel(Navigation, this);
        }
        public async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
    }
}