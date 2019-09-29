using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Shared.Models
{
    public class Price
    {
        public int Id                   { get; set; }
        public string Size              { get; set; }
        public decimal CustomerCost     { get; set; }
        public decimal SalonCost        { get; set; }

        [NotMapped]
        public decimal Cost             { get; set; }

        public virtual Product Product  { get; set; }
    }
}
