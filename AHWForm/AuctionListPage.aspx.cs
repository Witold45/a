using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using AHWForm.Models;
using LINQPad;

namespace AHWForm
{

    public partial class AuctionListPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuctionList.EnableDynamicData(typeof(Auction));
            
        }

        public IQueryable<AHWForm.Models.Auction> AuctionList_GetData()
        {
            int? value = null;
            string v = Request.QueryString["category"];
            value = Int32.Parse(v);
            
            AuctionContext auctionContext = new AuctionContext();
            IOrderedQueryable<Auction> ls;
            if(value == null)
            { 
                 ls = auctionContext.Auctions.OrderByDescending(x=>x.CategoryId);
            }
            else
            {
                List<int> lstOfCategories = GetChildrens(auctionContext.Auctions.OrderByDescending(x => x.CategoryId));
                ls = auctionContext.Auctions.Where(x => lstOfCategories.Contains(Convert.ToInt32(x.CategoryId))).OrderByDescending(x=>x.CategoryId);
            }
            return ls;
        }

        private List<int> GetChildrens(IOrderedQueryable<Auction> orderedQueryable)
        {
            throw new NotImplementedException();
        }

        
    }
}