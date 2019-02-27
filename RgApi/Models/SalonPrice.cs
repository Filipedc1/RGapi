using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Models
{
    public class SalonPrice
    {
        public int Id       { get; set; }
        public string Size  { get; set; }
        public double Cost  { get; set; }

        public virtual Product Product { get; set; }
    }
}
