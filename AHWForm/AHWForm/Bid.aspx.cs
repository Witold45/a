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
using AHWForm.Classes_And_Interfaces;

namespace AHWForm
{
    public partial class Bid : System.Web.UI.Page, IExtensionMethods
    {
        BidContext bidContext = new BidContext();
        private string Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Request.QueryString["Id"];
            if (!ExtensionMethods.CheckIfAuctionExist(Id,bidContext))
                Response.Redirect("/");

        }

        protected void BidSecond_Click(object sender, EventArgs e)
        {
            //Checking if textbox contains only numbers
                if (!System.Text.RegularExpressions.Regex.IsMatch(priceTextBox.Text, "[^0-9]"))
                {
                //Checking if value is right
                    if (PriceRange(priceTextBox.Text))
                    {  
                        Auction auction = bidContext.Auctions.Where(x => x.Id == Id).SingleOrDefault();
                        decimal price = Decimal.Parse(priceTextBox.Text);
                    //checking if bid is not to low
                    //If there is no bid pass bid
                    var maxBid = ExtensionMethods.GetMaxBidOfAuction(Id, bidContext, auction);
                    if (maxBid != null)
                    {
                        if (price > maxBid.Value)
                        {
                            PassAuction(price);
                        }
                        else
                        {
                            //throw info that your bid is too low
                            priceTextBox.BackColor = System.Drawing.Color.Red;
                        }

                    }
                    else
                    {
                        PassAuction(price);
                    }
                }
                }
                else
                {
                            priceTextBox.BackColor = System.Drawing.Color.Red;
                }       
        }

        private void PassAuction(decimal price)
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

            ExtensionMethods.RefreshDB();
            Response.Redirect("/AuctionDetails?Id=" + Id);
        }

        private bool PriceRange(string text)
        {
            //returns false if price is wrong or under 0
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

        


    }
}