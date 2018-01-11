using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using AHWForm.Models;
using System.Linq;
using System.Data;
using System.Data.Linq;
using MoreLinq;

namespace AHWForm
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        private System.Timers.Timer timer = new System.Timers.Timer(1000) { AutoReset = false };


        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            AuctionContext ac = new AuctionContext();
            BidContext bc = new BidContext();
            foreach(Auction item in ac.Auctions)
            {
                if(DateTime.Now >= item.DateCreated.AddDays(item.ExpiresIn))
                {
                    item.IsEnded = true;
                    item.WinnerId = SetWinnerID(item);
                }

                BidsModel MaxBid = GetMaxBidOfAuction(item.Id, bc, item);
                if(MaxBid != null)
                {
                    item.EndingPrice = MaxBid.Value;
                }

            }


            ac.SaveChanges();


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

        private string SetWinnerID(Auction auc)
        {
            BidContext bc = new BidContext();
            BidsModel bm = GetMaxBidOfAuction(auc.Id, bc);
            if (bm != null)
                return bm.AuctionId;
            else
                return null;
        }

        private BidsModel GetMaxBidOfAuction (string id, BidContext context)
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

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                var categories = GetCategories();

                PopulateNodes(categories, CategoriesTreeView);
                CategoriesTreeView.CollapseAll();
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


        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected List<Category> GetCategories()
        {
            CategoryContext catContext = new CategoryContext();
            var ls = catContext.Categories.ToList();
            return ls;       
        }


        protected void CategoriesTreeView_OnSelectedNodeChanged(object sender, EventArgs e)
        {
            var value = CategoriesTreeView.SelectedNode.Value;
            Response.Redirect("/AuctionListPage?category=" + value);
        }

        
    }

}