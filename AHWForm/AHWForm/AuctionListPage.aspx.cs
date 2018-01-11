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
            
            string v = Request.QueryString["category"];
            
            AuctionContext auctionContext = new AuctionContext();
            IQueryable<Auction> ls;
            //Redirect when v is null
            List<string> allCats = GetChildrens(v);
            allCats.Add(v);
            List<string> allCatsN = allCats.ConvertAll<string>(delegate(string x) { return x; });
            ls = auctionContext.Auctions.Where(t => allCatsN.Contains(t.CategoryId));
            return ls;
        }

        private List<string> GetChildrens(string id)
        {

            AuctionContext ac = new AuctionContext();
            Category cat = ac.Categories.Where(x => x.Id == id).SingleOrDefault();
            List<string> allChildrenList = new List<string>();
            allChildrenList = FoundChildren(allChildrenList, cat.Id,cat, ac);
            return allChildrenList;
        }


        private List<string> FoundChildren(List<string> listOfCatId, string parentId, Category cat, AuctionContext ac)
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