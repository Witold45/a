using AHWForm.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AHWForm
{
    public partial class AuctionDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Todo handle empty or wrong auction data
            if (!Page.IsPostBack)
            {
                InitUI();
                Auction auction = GetAuction(Request.QueryString["Id"]);
                if (auction != null)
                    setProperties(auction);
                else
                    Response.Redirect("/");
            }
        }

        private void setProperties(Auction auction)
        {
            AuctionTitle.Text = auction.Title;
            AuctionShortDescription.Text = auction.Description;
            AuctionLongDescription.Text = auction.LongDescription;
            Price.Text = auction.EndingPrice.ToString("G");
            CreatorName.Text = GetUserNameByID(auction.CreatorId);
        }

        private string GetUserNameByID(string creatorId)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(creatorId).UserName;
        }

        private void InitUI()
        {
            //AuctionTitle.Font.Size = 34;
        }

        private Auction GetAuction(string id)
        {
            try
            {
                int idInt = Int32.Parse(id);
                AuctionContext auctionContext = new AuctionContext();
                Auction actAuction = auctionContext.Auctions.Where(s => s.Id == idInt).SingleOrDefault();
                return actAuction;
            }
            catch
            {
                return null;
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