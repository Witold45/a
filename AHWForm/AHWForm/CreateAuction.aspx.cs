using AHWForm.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AHWForm
{
    public partial class CreateAuction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var categories = GetCategories();
                PopulateNodes(categories, NewAuctionTreeView);
                NewAuctionTreeView.CollapseAll();
                for(int i = 1; i < 14; i++)
                {
                    ExpiresInDropDown.Items.Add(i.ToString());
                }
                
                
                
            }
        }

        

        protected void PassNewAuctionButton_Click(object sender, EventArgs e)
        {
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

        private void PopulateNodes(List<Category> categories, TreeView tw)
        {
            foreach (var item in categories)
            {
                if (item.ParentCategoryId == null)
                {
                    var rootNode = new TreeNode(item.Name, item.Id.ToString());
                    AddChildren(categories, rootNode);
                    tw.Nodes.Add(rootNode);
                }

            }
        }

        private void AddChildren(List<Category> categories, TreeNode activeTreeNode)
        {
            foreach (var item in categories)
            {
                if (item.ParentCategoryId == activeTreeNode.Value)
                {
                    TreeNode tn = new TreeNode(item.Name, item.Id.ToString());

                    AddChildren(categories, tn);
                    activeTreeNode.ChildNodes.Add(tn);
                }
            }

        }

        protected List<Category> GetCategories()
        {
            CategoryContext catContext = new CategoryContext();
            var ls = catContext.Categories.ToList();
            return ls;
        }
    }
}