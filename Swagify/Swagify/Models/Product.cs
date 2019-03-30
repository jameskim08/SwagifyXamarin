using System;
using System.Collections.Generic;
using System.Text;

namespace Swagify.Models
{
    class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string CheckoutUrl { get; set; }
    }
}
