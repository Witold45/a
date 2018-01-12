using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHWForm.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MoreLinq;
using System.Web.UI.WebControls;
using System;

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

        public static Auction GetAuction(string id)
        {
            try
            {

                AuctionContext auctionContext = new AuctionContext();
                Auction actAuction = auctionContext.Auctions.Where(s => s.Id == id).SingleOrDefault();
                return actAuction;
            }
            catch
            {
                return null;
            }

        }

        public static List<Category> GetCategories()
        {
            CategoryContext catContext = new CategoryContext();
            var ls = catContext.Categories.ToList();
            return ls;
        }


        public static BidsModel GetMaxBidOfAuction(string id, BidContext context, Auction auction)
        {
            List<BidsModel> bids = context.Bids.Where(x => x.AuctionId == id).ToList();
            if (bids.Count > 0)
            {
                BidsModel bm = bids.MaxBy(x => x.Value);
                return bm;
            }
            else
                return null;
        }

        public static BidsModel GetMaxBidOfAuction(string id, BidContext context)
        {
            List<BidsModel> bids = context.Bids.Where(x => x.AuctionId == id).ToList();
            if (bids.Count > 0)
            {
                BidsModel bm = bids.MaxBy(x => x.Value);
                return bm;
            }
            else
                return null;
        }

        public static bool CheckIfAuctionExist(string id, BidContext bidContext)
        {
            return bidContext.Auctions.Any(x => x.Id == id);
        }

        public static void PopulateNodes(List<Category> categories, TreeView tw)
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

        public static void AddChildren(List<Category> categories, TreeNode activeTreeNode)
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

        public static string SetWinnerID(Auction auc)
        {
            BidContext bc = new BidContext();
            BidsModel bm = GetMaxBidOfAuction(auc.Id, bc);
            if (bm != null)
                return bm.AuctionId;
            else
                return null;
        }

        public static void RefreshDB()
        {
            AuctionContext ac = new AuctionContext();
            BidContext bc = new BidContext();
            foreach (Auction item in ac.Auctions)
            {
                if (DateTime.Now >= item.DateCreated.AddDays(item.ExpiresIn))
                {
                    item.IsEnded = true;
                    item.WinnerId = ExtensionMethods.SetWinnerID(item);
                }

                BidsModel MaxBid = ExtensionMethods.GetMaxBidOfAuction(item.Id, bc, item);
                if (MaxBid != null)
                {
                    item.EndingPrice = MaxBid.Value;
                }

            }
            ac.SaveChanges();
        }
    }
}