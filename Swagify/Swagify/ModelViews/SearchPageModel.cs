using Swagify.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Swagify
{
    class SearchPageModel : BaseViewModel
    {
        HttpSearchRequest httpSearchRequest = new HttpSearchRequest(App.HttpClient);
        HttpWishlistRequest httpWishlistRequest = new HttpWishlistRequest(App.HttpClient);

        public SearchPageModel(INavigation navigation, Page page) : base(navigation, page) {
            Name = "James";

            Task.Run(async () => {
                Wishlist = await GetWishlist();
                OnPropertyChanged(nameof(Wishlist));
            });
            this.SearchProductCommand = new Command(SearchProduct);
            this.AddToWishlistCommand = new Command<Product>(AddToWishlist);
            this.MoveToBrandCommand = new Command<string>(MoveToBrand);
            this.DeleteFromWishlistCommand = new Command<Product>(DeleteFromWishlist);
        }

        private async void AddToWishlist(Product product)
        {
            var statusCode = await httpWishlistRequest.PostProductToWishlist(product.Id);
            if (statusCode == HttpStatusCode.Created)
            {
                await Page.DisplayAlert("Success", String.Format("{0} has been added to your wishlist!", product.Name), "OK");
                Wishlist = await GetWishlist();
                OnPropertyChanged(nameof(Wishlist));
            } else
            {
                await Page.DisplayAlert("err", String.Format("{0} something went wrong!", product.Name), "OK");
            }
        }

        private async void DeleteFromWishlist(Product product)
        {
            var statusCode = await httpWishlistRequest.DeleteProductFromWishlist(product.Id);
            if (statusCode == HttpStatusCode.Created)
            {
                await Page.DisplayAlert("Success", String.Format("{0} has been deleted from your wishlist!", product.Name), "OK");
                Wishlist = await GetWishlist();
                OnPropertyChanged(nameof(Wishlist));
            }
            else
            {
                await Page.DisplayAlert("err", String.Format("{0} something went wrong!", product.Name), "OK");
            }
        }

        private async void SearchProduct()
        {
            try
            {
                var productList = await httpSearchRequest.GetSearchList(SearchText);
                if (productList.Count == 0)
                {
                    await Page.DisplayAlert("Error", "Sorry, no matches found.", "OK");
                }
                else
                {
                    ProductList = new ObservableCollection<Product>(productList);
                    OnPropertyChanged(nameof(ProductList));
                }
            }
            catch (Exception error)
            {
                await Page.DisplayAlert("Error", String.Format("{0}. Something went wrong. Please try again later.", error.Message), "OK");

            }
        }

        private void MoveToBrand(string uriString)
        {
            Uri uri = new Uri(uriString);
            Device.OpenUri(uri);
        }

        private async Task<ObservableCollection<Product>> GetWishlist()
        {
            return new ObservableCollection<Product>(await httpWishlistRequest.GetWishlist());
        }
        
        public ICommand SearchProductCommand { get; }
        public ICommand AddToWishlistCommand { get; }
        public ICommand MoveToBrandCommand { get; }
        public ICommand DeleteFromWishlistCommand { get; }
        public ObservableCollection<Product> Wishlist { get; set; }
        public ObservableCollection<Product> ProductList { get; set; }
        public string Name { get; set; }
        public string SearchText { get; set; }
    }
}
