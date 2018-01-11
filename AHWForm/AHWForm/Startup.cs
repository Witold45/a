using System;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Web.Caching;
using AHWForm.Models;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

[assembly: OwinStartupAttribute(typeof(AHWForm.Startup))]
namespace AHWForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            AddTask("DoStuff", 60);
        }

        private static CacheItemRemovedCallback OnCacheRemove = null;

        protected void Application_Start(object sender, EventArgs e)
        {
            AddTask("RefreshAuctions", 60);
        }

        private void AddTask(string name, int seconds)
        {
            OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
            HttpRuntime.Cache.Insert(name, seconds, null,
                DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, OnCacheRemove);
        }

        public void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            AuctionContext ac = new AuctionContext();
            BidContext bc = new BidContext();
            foreach (Auction item in ac.Auctions)
            {
                if (DateTime.Now >= item.DateCreated.AddDays(item.ExpiresIn))
                {
                    item.IsEnded = true;
                    item.WinnerId = SetWinnerID(item);
                }

                BidsModel MaxBid = GetMaxBidOfAuction(item.Id, bc, item);
                if (MaxBid != null)
                {
                    item.EndingPrice = MaxBid.Value;
                }

            }


            ac.SaveChanges();
            AddTask(k, Convert.ToInt32(v));
        }

        private BidsModel GetMaxBidOfAuction(string id, BidContext context, Auction auction)
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

        private BidsModel GetMaxBidOfAuction(string id, BidContext context)
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

        private string SetWinnerID(Auction auc)
        {
            BidContext bc = new BidContext();
            BidsModel bm = GetMaxBidOfAuction(auc.Id, bc);
            if (bm != null)
                return bm.AuctionId;
            else
                return null;
        }
    }
}
