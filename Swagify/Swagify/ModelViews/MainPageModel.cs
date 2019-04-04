using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swagify
{
    class MainPageModel : BaseViewModel
    {
        public MainPageModel(INavigation navigation, Page page) : base(navigation, page) {
            SearchPageModel = new SearchPageModel(navigation, page);
            this.UpgradeCommand = new Command(() => Upgrade());

        }

        public async void Upgrade()
        {
            string header = "Upgrade to premium for more features including:";
            string perks = "- More websites to search \n\n- Request new websites to add \n\n- Sorting and filtering options";
            string message = header + "\n\n" + perks + "\n\nComing soon...";

            await Page.DisplayAlert("Upgrade to Premium", message, "OK");
        }

        public SearchPageModel SearchPageModel { get; set; }
        public ICommand UpgradeCommand { protected set; get; }

    }
}
