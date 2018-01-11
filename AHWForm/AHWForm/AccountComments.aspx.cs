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
    public partial class AccountComments : System.Web.UI.Page, IExtensionMethods 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                RedirectToLoginPage();
            CommentList.DataSource = AuctionList_GetData();
            CommentList.DataBind();

        }

        private List<ExtendentComments> AuctionList_GetData()
        {
            //returns list of comments connected to user
            CommentsContext commentsContext = new CommentsContext();
            string ID = HttpContext.Current.User.Identity.GetUserId();
            List<Auction> assAuctionList = GetListOfAssAuctions(ID, commentsContext).ToList();
            List<ExtendentComments> extendentCommentsList = new List<ExtendentComments>();

            foreach(Auction item in assAuctionList)
            {
                ExtendentComments row = new ExtendentComments()
                {
                    AuctionTitle = item.Title,
                    AuctionUrl = "AuctionDetails.aspx?Id=" + item.Id,
                    SellerUrl = "CommentSite.aspx?Id=" + item.CreatorId,
                    SellerName = ExtensionMethods.GetUserNameByID(item.CreatorId),
                };
                if (item.IsEnded)
                {
                    row.Id = ExtensionMethods.GetCommentFromAuction(item.Id, ID, commentsContext).Id;
                    row.Rate = ExtensionMethods.GetCommentFromAuction(item.Id, ID, commentsContext).Rate;
                    row.Comment = ExtensionMethods.GetCommentFromAuction(item.Id, ID, commentsContext).Description;
                }
                extendentCommentsList.Add(row);
            }
            return extendentCommentsList;
        }

        private IQueryable<Auction> GetListOfAssAuctions(string id, CommentsContext commentsContext)
        {
            //returns query that gives auctions that are connected to actual user 
            IQueryable<Auction> auctions;
            var MaxBids = ExtensionMethods.GetMaxBidsPerUser(ID, commentsContext);
            auctions = commentsContext.Auctions.Where(t => t.CreatorId == ID);
            auctions = auctions.Where(t => MaxBids.Contains(t.Id));
            return auctions;
        }

        private void RedirectToLoginPage()
        {
            Response.Redirect("/Account/Login");
        }

        protected void AddComment_Click(object sender, EventArgs e)
        {
            //Todo
        }
    }
}