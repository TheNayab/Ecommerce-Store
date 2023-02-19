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
    }
}