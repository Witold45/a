using AHWForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AHWForm
{
    public class ExtendentComments
    {
        public string Id { get; set; }
        public string AuctionUrl { get; set; }
        public string AuctionTitle { get; set; }
        public string BuyerUrl { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public string SellerUrl { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}