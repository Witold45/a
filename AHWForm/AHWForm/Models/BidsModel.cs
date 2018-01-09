using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AHWForm.Models
{
    public class BidsModel
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public string UserId { get; set; }
        public int AuctionId { get; set; }
    }

    public class BidContext : DbContext
    {
        public DbSet<BidsModel> Bids { get; set; }
     
    }
}