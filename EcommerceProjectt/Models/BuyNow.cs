using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceProjectt.Models
{
    public class BuyNow
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public int CardId { get; set; }
        public int CreId { get; set; }
        public string MerId { get; set; }
        public string Img { get; set; }
        public string NameOfProduct { get; set; }
        public string PriceOfProduct { get; set; }
        public int intallments { get; set; }
        public int totalinstall { get; set; }

        public string Confirm { get; set; }

    }
    
}