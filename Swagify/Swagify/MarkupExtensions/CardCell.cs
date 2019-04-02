using FFImageLoading.Forms;
using Swagify.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Swagify
{
    public class CardCell : ViewCell
    {
        readonly CachedImage cachedImage = null;

        public CardCell()
        {
            cachedImage = new CachedImage();
            View = cachedImage;
        }

        protected override void OnBindingContextChanged()
        {
            // you can also put cachedImage.Source = null; here to prevent showing old images occasionally
            cachedImage.Source = null;
            var item = BindingContext as Product;

            if (item == null)
            {
                return;
            }

            cachedImage.Source = item.ImageUrl;

            base.OnBindingContextChanged();
        }
    }
}
