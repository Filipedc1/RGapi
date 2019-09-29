using RgApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Shared.Models
{
    public class CartItem
    {
        public int CartItemId           { get; set; }
        public int ProductId            { get; set; }
        public string Name              { get; set; }
        public string Description       { get; set; }
        public string ImageUrl          { get; set; }
        public int Quantity             { get; set; }

        public Price Price              { get; set; }

        public virtual AppUser User     { get; set; }
    }
}
