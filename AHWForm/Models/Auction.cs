using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AHWForm.Models
{
    public class Auction
    {
        [ScaffoldColumn(false)]        
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public decimal StartPrice { get; set; }
        public decimal EndingPrice { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }
        [ScaffoldColumn(false)]
        public int ExpiresIn { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public bool IsEnded { get; set; }
        //public List<Bid> Bids { get; set; }
        [ScaffoldColumn(false)]
        public string CreatorId { get; set; }
    }

    public class AuctionContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}