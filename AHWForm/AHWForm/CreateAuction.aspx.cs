using AHWForm.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AHWForm.Classes_And_Interfaces;

namespace AHWForm
{
    public partial class CreateAuction : System.Web.UI.Page, IExtensionMethods
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                RedirectToLoginPage();
            
            if (!this.IsPostBack)
            {
                var categories = ExtensionMethods.GetCategories();
                ExtensionMethods.PopulateNodes(categories, NewAuctionTreeView);
                NewAuctionTreeView.CollapseAll();
                for(int i = 1; i < 14; i++)
                {
                    ExpiresInDropDown.Items.Add(i.ToString());
                }                
            }
        }

        private void RedirectToLoginPage()
        {
            Response.Redirect("/Account/Login");
        }

        protected void PassNewAuctionButton_Click(object sender, EventArgs e)
        {
            //If everything is fine create new auction and redirect to it 
            if (User.Identity.IsAuthenticated)
            { 
                AuctionContext auctionContext = new AuctionContext();
                Auction auction = new Auction()
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = NewAuctionTreeView.SelectedNode.Value,
                    Title = AuctionTitleTextBox.Text,
                    StartPrice = Decimal.Parse(MinimalPriceTextBox.Text),
                    EndingPrice = Decimal.Parse(MinimalPriceTextBox.Text),
                    DateCreated = DateTime.Now,
                    ExpiresIn = Convert.ToInt32(ExpiresInDropDown.SelectedItem.Value),
                    Description = DescriptionTextBox.Text,
                    IsEnded = false,
                    CreatorId = HttpContext.Current.User.Identity.GetUserId(),
                    LongDescription = DescriptionLongTextBox.Text,
                };

                auctionContext.Auctions.Add(auction);
                auctionContext.SaveChanges();
                Response.Redirect("/AuctionDetails?Id=" + auction.Id);
            }
            else
            {
                Response.Redirect("/Account/Login");
            }
        }
    }
}