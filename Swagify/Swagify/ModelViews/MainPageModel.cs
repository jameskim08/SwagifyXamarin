using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Swagify
{
    class MainPageModel : BaseViewModel
    {
        public MainPageModel(INavigation navigation, Page page) : base(navigation, page) {
            SearchPageModel = new SearchPageModel(navigation, page);
        }

        public SearchPageModel SearchPageModel { get; set; }
    }
}
