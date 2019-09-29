using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Shared.Models
{
    public enum CardType { Credit, Debit }
    public class PaymentDetail
    {
        public int PaymentDetailId      { get; set; }
        public string CardOwnerName     { get; set; }
        public string CardNumber        { get; set; }
        public DateTime Expiration      { get; set; }
        public int CVV                  { get; set; }
        public CardType CardType        { get; set; }
    }
}
