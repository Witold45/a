using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using AHWForm.Models;


namespace AHWForm
{

    public partial class AuctionListPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            AuctionList.EnableDynamicData(typeof(Auction));
            
        }

        public IQueryable<Auction> AuctionList_GetData()
        {
            //TODO Categories fetch
            int? value = null;
            string v = Request.QueryString["category"];
            if(v!=null)
            value = Int32.Parse(v);
            
            AuctionContext auctionContext = new AuctionContext();
            IQueryable<Auction> ls;
            
            List<int> allCats = GetChildrens(value);
            allCats.Add((int)value);
            List<int?> allCatsN = allCats.ConvertAll<int?>(delegate(int x) { return x; });
            var temp = allCats.Contains(7);
            ls = auctionContext.Auctions.Where(t => allCatsN.Contains(t.CategoryId));
            var temp2 = ls.ToList();
            return ls;
        }

        private List<int> GetChildrens(int? id)
        {
            AuctionContext ac = new AuctionContext();
            Category cat = ac.Categories.Where(x => x.Id == id).SingleOrDefault();
            List<int> allChildrenList = new List<int>();
            //if(cat.Id!=null)
            allChildrenList = FoundChildren(allChildrenList, cat.Id,cat, ac);
            return allChildrenList;
        }


        private List<int> FoundChildren(List<int> listOfCatId, int parentId, Category cat, AuctionContext ac)
        {
            List<Category> categories = ac.Categories.ToList();
            foreach (var item in categories)
            {
                if (item.ParentCategoryId == parentId)
                {
                    listOfCatId.Add(item.Id);
                    //if(cat.Id!=null)
                    listOfCatId.AddRange(FoundChildren(listOfCatId, item.Id, cat, ac));
                }
                    
            }
            return listOfCatId;
        }

    }
}