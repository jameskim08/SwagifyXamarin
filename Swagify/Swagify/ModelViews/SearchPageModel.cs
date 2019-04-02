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
        private bool loadingWishlist, loadingProductList;
        private ObservableCollection<Product> wishlist, productList;

        public SearchPageModel(INavigation navigation, Page page) : base(navigation, page) {
            Name = ((User)Application.Current.Properties["user"]).Name;
            LoadingWishlist = false;
            LoadingProductList = false;
            Task.Run(async () => {
                LoadingWishlist = true;
                Wishlist = await GetWishlist();
                LoadingWishlist = false;
            });
            this.SearchProductCommand = new Command(SearchProduct);
            this.AddToWishlistCommand = new Command<Product>(AddToWishlist);
            this.MoveToBrandCommand = new Command<string>(MoveToBrand);
            this.DeleteFromWishlistCommand = new Command<Product>(DeleteFromWishlist);
        }

        private async void AddToWishlist(Product product)
        {
            bool answer = await Page.DisplayAlert("Confirm", String.Format("Add {0} to your wishlist?", product.Name), "Yes", "Cancel");
            if (answer)
            {
                var statusCode = await httpWishlistRequest.PostProductToWishlist(product.Id);
                if (statusCode == HttpStatusCode.Created)
                {
                    LoadingWishlist = true;
                    Wishlist = await GetWishlist();
                    LoadingWishlist = false;
                    await Page.DisplayAlert("Success", String.Format("{0} has been added to your wishlist!", product.Name), "OK");
                }
                else if (statusCode == HttpStatusCode.Conflict)
                {
                    await Page.DisplayAlert("Error", String.Format("{0} is already on your wishlist!", product.Name), "OK");
                }
                else
                {
                    await Page.DisplayAlert("Error", String.Format("Something went wrong! Please try again later.", product.Name), "OK");
                }
            }
        }

        private async void DeleteFromWishlist(Product product)
        {
            bool answer = await Page.DisplayAlert("Warning", String.Format("Are you sure you want to delete {0} from your wishlist? This is not reversible.", product.Name), "Yes", "Cancel");
            if (answer)
            {
                var statusCode = await httpWishlistRequest.DeleteProductFromWishlist(product.Id);
                if (statusCode == HttpStatusCode.Created)
                {
                    await Page.DisplayAlert("Success", String.Format("{0} has been deleted from your wishlist!", product.Name), "OK");
                    Wishlist = await GetWishlist();
                }
                else
                {
                    await Page.DisplayAlert("Error", String.Format("Something went wrong! Please try again later.", product.Name), "OK");
                }
            }
        }

        private async void SearchProduct()
        {
            if (SearchText.Length < 3 || string.IsNullOrWhiteSpace(SearchText))
            {
                await Page.DisplayAlert("Error", "Search must be at least 3 letters.", "OK");
            }
            else
            {
                try
                {
                    LoadingProductList = true;

                    var searchList = await httpSearchRequest.GetSearchList(SearchText);
                    if (searchList.Count == 0)
                    {
                        LoadingProductList = false;
                        await Page.DisplayAlert("Search", "Sorry, no matches were found.", "OK");
                    }
                    else
                    {
                        ProductList = new ObservableCollection<Product>(searchList);
                        LoadingProductList = false;
                    }
                }
                catch (Exception e)
                {
                    await Page.DisplayAlert("Error", String.Format("Something went wrong. Please try again later."), "OK");
                    Console.WriteLine(e.Message);
                    LoadingProductList = false;
                }
            }
        }

        private async void MoveToBrand(string uriString)
        {
            bool answer = await Page.DisplayAlert("Confirm", "Move to product website?", "Yes", "Cancel");

            if (answer)
            {
                try
                {
                    Uri uri = new Uri(uriString);
                    Device.OpenUri(uri);
                }
                catch (Exception e)
                {
                    await Page.DisplayAlert("Error", "Something went wrong. Please try again later.", "OK");
                    Console.WriteLine(e.Message);
                }
            }
        }

        private async Task<ObservableCollection<Product>> GetWishlist()
        {
            return new ObservableCollection<Product>(await httpWishlistRequest.GetWishlist());
        }
        
        public ICommand SearchProductCommand { get; }
        public ICommand AddToWishlistCommand { get; }
        public ICommand MoveToBrandCommand { get; }
        public ICommand DeleteFromWishlistCommand { get; }
        public ObservableCollection<Product> Wishlist {
            get { return wishlist; }
            set
            {
                if (wishlist != value)
                {
                    wishlist = value;
                    OnPropertyChanged("Wishlist");
                }
            }
        }
        public ObservableCollection<Product> ProductList
        {
            get { return productList; }
            set
            {
                if (productList != value)
                {
                    productList = value;
                    OnPropertyChanged("ProductList");
                }
            }
        }
        public bool LoadingWishlist
        {
            get { return loadingWishlist; }
            set
            {
                if (loadingWishlist != value)
                {
                    loadingWishlist = value;
                    OnPropertyChanged("LoadingWishlist");
                }
            }
        }
        public bool LoadingProductList
        {
            get { return loadingProductList; }
            set
            {
                if (loadingProductList != value)
                {
                    loadingProductList = value;
                    OnPropertyChanged("LoadingProductList");
                }
            }
        }
        public string Name { get; set; }
        public string SearchText { get; set; }
    }
}
