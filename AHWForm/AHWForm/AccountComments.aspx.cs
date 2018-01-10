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
    public partial class AccountComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CommentList.DataSource = AuctionList_GetData();
            CommentList.DataBind();

        }

        public List<ExtendentComments> AuctionList_GetData()
        {
            CommentsContext commentsContext = new CommentsContext();
            ID = HttpContext.Current.User.Identity.GetUserId();
            List<Auction> assAuctionList = GetListOfAssAuctions(ID, commentsContext).ToList();
            List<ExtendentComments> extendentCommentsList = new List<ExtendentComments>();
            foreach(Auction item in assAuctionList)
            {
                ExtendentComments row = new ExtendentComments()
                {
                    AuctionTitle = item.Title,
                    AuctionUrl = "AuctionDetails.aspx?Id=" + item.Id,
                    //BuyerUrl = "CommentSite.aspx?Id=" + item.BuyerId,
                    SellerUrl = "CommentSite.aspx?Id=" + item.CreatorId,
                    SellerName = GetUserNameByID(item.CreatorId),
                };
                if (item.IsEnded)
                {
                    row.Id = GetCommentFromAuction(item.Id, ID, commentsContext).Id;
                    row.Rate = GetCommentFromAuction(item.Id, ID, commentsContext).Rate;
                    row.Comment = GetCommentFromAuction(item.Id, ID, commentsContext).Description;
                }
                extendentCommentsList.Add(row);
            }
            return extendentCommentsList;
        }

        private Comments GetCommentFromAuction(string auctionId, string userId, CommentsContext context)
        {
            return context.Comment.Where(x => x.AuctionId == auctionId && x.BuyerId == userId).SingleOrDefault();
        }

        private string GetUserNameByID(string creatorId)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(creatorId).UserName;
        }

        private IQueryable<Auction> GetListOfAssAuctions(string id, CommentsContext commentsContext)
        {
            IQueryable<Auction> auctions;
            var MaxBids = GetMaxBidsPerUser(ID, commentsContext);
            auctions = commentsContext.Auctions.Where(t => t.CreatorId == ID);
            auctions = auctions.Where(t => MaxBids.Contains(t.Id));
            return auctions;
        }

        private Auction GetAuctionObject(string auctionId)
        {
            AuctionContext auctionContext = new AuctionContext();
            Auction auction = auctionContext.Auctions.Where(s => s.Id == auctionId).SingleOrDefault();
            return auction;
        }

        private List<string> GetMaxBidsPerUser(string id, CommentsContext context)
        {
            List<BidsModel> bids = context.Bids.Where(x => x.UserId == id).ToList();
            var maxBids = from e in bids
                          group e by e.UserId into Auct
                          let top = Auct.Max(x => x.Value)
                          select new BidsModel
                          {
                              UserId = Auct.Key,
                              AuctionId = Auct.First(y => y.Value == top).AuctionId,
                              Value = top,
                              Id = Auct.First(y => y.Value == top).Id,
                          };
            return maxBids.Select(t=>t.AuctionId).ToList();
              
        }

        protected void AddComment_Click(object sender, EventArgs e)
        {

        }
    }
}