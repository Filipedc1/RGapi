using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Models
{
    public class OrderDetail
    {
        public int OrderDetailId        { get; set; }
        public int ProductId            { get; set; }
        public string ProductName       { get; set; }
        public int ProductQuantity      { get; set; }
        public string ProductSize       { get; set; }
        public decimal ProductCost      { get; set; }

        public Price ProductPrice       { get; set; }
        public Order Order              { get; set; }
    }
}
