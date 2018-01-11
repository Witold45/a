using AHWForm.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AHWForm
{
    public partial class Bid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void BidSecond_Click(object sender, EventArgs e)
        {
            //ToRefactor
            //Add code that prevent bidCreator from bidding his own auction
            string Id = Request.QueryString["Id"];
            if (!String.IsNullOrEmpty(Id))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(priceTextBox.Text, "[^0-9]"))
                {
                    if (PriceRange(priceTextBox.Text))
                    {
                        BidContext bidContext = new BidContext();
                        Auction auction = bidContext.Auctions.Where(x => x.Id == Id).SingleOrDefault();
                    decimal price = Decimal.Parse(priceTextBox.Text);   

                    if (price > GetMaxBidOfAuction(Id, bidContext, auction))
                    {
                        priceTextBox.BackColor = System.Drawing.Color.Empty;
                        BidsModel bidsModel = new BidsModel()
                        {
                            AuctionId = Id,
                            UserId = HttpContext.Current.User.Identity.GetUserId(),
                            Id = Guid.NewGuid().ToString(),
                            Value = price,
                        };

                        bidContext.Bids.Add(bidsModel);
                        bidContext.SaveChanges();
                        Response.Redirect("/AuctionDetails?Id=" + Id);
                        }
                    else
                    {
                        //throw info that your bid is too low
                        priceTextBox.BackColor = System.Drawing.Color.Red;
                    }
                }
                }
                else
                {
                    //throw info that format is wrong
                }
            }
            else
            {
                
            }

        }

        private bool PriceRange(string text)
        {
            decimal price;
            try
            {
                price = Decimal.Parse(text);
            }
            catch
            {
                return false;
            }
            if (price < 0)
                return false;
            return true;
        }

        private decimal GetMaxBidOfAuction(string id, BidContext context, Auction auction)
        {
            List<BidsModel> bids = context.Bids.Where(x => x.AuctionId == id).ToList();
            if (bids.Count > 0)
            {
                BidsModel bm = bids.MaxBy(x => x.Value);
                return bm.Value;
            }
            else
                return auction.EndingPrice;
        }


    }
}