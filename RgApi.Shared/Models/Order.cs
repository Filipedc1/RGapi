using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Shared.Models
{
    public class Order
    {
        public int OrderId                                      { get; set; }
        public decimal Total                                    { get; set; }
        public DateTime Placed                                  { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails    { get; set; }
        public virtual BillingDetail BillingDetail              { get; set; }
        public virtual AppUser User                             { get; set; }
    }
}
