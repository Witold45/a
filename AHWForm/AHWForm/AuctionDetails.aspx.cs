using AHWForm.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AHWForm.Classes_And_Interfaces;

namespace AHWForm
{
    public partial class AuctionDetails : System.Web.UI.Page, IExtensionMethods
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Todo handle empty or wrong auction data
            if (!Page.IsPostBack)
            {
                Auction auction = ExtensionMethods.GetAuction(Request.QueryString["Id"]);
                if (auction != null)
                    setProperties(auction);
                else
                    Response.Redirect("/");
            }
        }

        private void setProperties(Auction auction)
        {
            //Setting properties in current site
            AuctionTitle.Text = auction.Title;
            AuctionShortDescription.Text = auction.Description;
            AuctionLongDescription.Text = auction.LongDescription;
            
            Price.Text = auction.EndingPrice.ToString("G");
            CreatorName.Text = ExtensionMethods.GetUserNameByID(auction.CreatorId);
            AuctionCreatedLabel.Text = auction.DateCreated.ToString();
            if (!auction.IsEnded) {
                AuctionExpiresLabel.Text = auction.DateCreated.AddDays(auction.ExpiresIn).ToString();
                if (auction.CreatorId != HttpContext.Current.User.Identity.GetUserId())
                    Bid.Visible = true;
                else
                    Bid.Visible = false;
                AuctionExpiresLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                AuctionExpiresLabel.Text = GetLocalResourceObject("AuctionEndedText").ToString();
                AuctionExpiresLabel.ForeColor = System.Drawing.Color.Red;
                Bid.Visible = false;
            }

        }

        protected void Bid_Click(object sender, EventArgs e)
        {
            
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login.aspx"); 
            else
                Response.Redirect("Bid?Id=" + (Request.QueryString["Id"]));
            
        }


    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           