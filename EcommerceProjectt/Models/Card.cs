using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceProjectt.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
        public string MerchantId { get; set; }
        public int Quantity { get; set; }
        public static int Total { get; set; }
        public  int Increment { get; set; }
        public string PayNow { get; set; }
    }
}