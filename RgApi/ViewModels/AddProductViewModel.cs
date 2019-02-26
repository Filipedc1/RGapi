using RgApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.ViewModels
{
    public class AddProductViewModel
    {
        public string Name          { get; set; }
        public string Description   { get; set; }
        public string ImageUrl      { get; set; }

        public IEnumerable<CustomerPrice> CustomerPrices { get; set; }
        public IEnumerable<SalonPrice> SalonPrices { get; set; }

        //public IFormFile ImageUpload { get; set; }
    }
}
