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
    public partial class CommentSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<CommentsView> comments = GetComments(Request.QueryString["Id"]);
                if (comments != null)
                    fetchData(comments);
                else
                    Response.Redirect("/");
            }

        }

        private void fetchData(List<CommentsView> comments)
        {
            CommentList.DataSource = comments;
            CommentList.DataBind();

           
        }

        private List<CommentsView> GetComments(string id)
        {
                CommentsContext commentsContext = new CommentsContext();
                IQueryable<Comments> query =  commentsContext.Comment.Where(x => x.SellerId == id);
                var test = commentsContext.Comment.FirstOrDefault();
                List<Comments> commentsList = query.ToList();
                List<CommentsView> commentsView = new List<CommentsView>();
                foreach(Comments item in commentsList)
                {      
                    CommentsView row = new CommentsView();
                    row.AuctionUrl = "AuctionDetails.aspx?Id=" + item.AuctionId;
                    Auction auction = GetAuctionObject(item.AuctionId);
                    row.AuctionTitle = GetAuctionObject(item.AuctionId).Title;
                    row.BuyerUrl = "CommentSite.aspx?Id=" + item.BuyerId;
                    row.SellerUrl = "CommentSite.aspx?Id=" + item.SellerId;
                    row.Description = item.Description;
                    row.Rate = item.Rate;
                    row.SellerName = GetUserNameByID(item.SellerId);
                    row.BuyerName = GetUserNameByID(item.BuyerId);
                    commentsView.Add(row);
                }
                return commentsView;
        }

        private string GetUserNameByID(string creatorId)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(creatorId).UserName;
        }

        private Auction GetAuctionObject(int auctionId)
        {
            AuctionContext auctionContext = new AuctionContext();
            Auction auction = auctionContext.Auctions.Where(s => s.Id == auctionId).SingleOrDefault();
            return auction;
        }

        protected void AuctionIdLoad(object sender, EventArgs e)
        {
            Label actualLabel = sender as Label;
        }

        protected void auctionLinkLoad(object sender, EventArgs e)
        {

        }

        protected void userLinkClick(object sender, EventArgs e)
        {

        }
    }
}