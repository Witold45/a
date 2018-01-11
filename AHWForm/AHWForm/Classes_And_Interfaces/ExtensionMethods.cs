using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHWForm.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AHWForm.Classes_And_Interfaces
{
    public class ExtensionMethods
    {
        
        public static List<string> GetMaxBidsPerUser(string id, CommentsContext context)
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
            return maxBids.Select(t => t.AuctionId).ToList();
        }
        public static string GetUserNameByID(string creatorId)
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(creatorId).UserName;
        }

        public static Comments GetCommentFromAuction(string auctionId, string userId, CommentsContext context)
        {
            return context.Comment.Where(x => x.AuctionId == auctionId && x.BuyerId == userId).SingleOrDefault();
        }

    }
}