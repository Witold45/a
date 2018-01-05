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

namespace AHWForm
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        

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
                
                PopulateTreeView(categories, 0, null);
            }
        }

        private void PopulateTreeView(List<Category> categories, int parentId, TreeNode treeNode)
        {
            try
            {
                Page.Header.Title = "Organogram Picker";
                

                foreach(Category item in categories)
                {
                    TreeNode rootNode = new TreeNode(item.Id.ToString(), item.Name);
                    CategoriesTreeView.Nodes.Add(rootNode);
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void BindChilds(List<Category> categories, TreeNode node, int parentNodeID)
        {

            string str = "Select NodeID as ID , Name   From RAHierarchyNode where ParentNodeID=" + parentNodeID;

            foreach(Category item in categories) { 
                TreeNode childNode = new TreeNode(item.Id.ToString(), item.Name);
                node.ChildNodes.Add(childNode);
            }

        }

        protected void CategoriesTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (!(CategoriesTreeView.SelectedNode.ChildNodes.Count > 0))
            {
                var categories = GetCategories();
                BindChilds(categories, CategoriesTreeView.SelectedNode, Convert.ToInt32(CategoriesTreeView.SelectedNode.Value));
                CategoriesTreeView.SelectedNode.Expand();
            }
        }


        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected List<Category> GetCategories()
        {

            //return categoryContext.Categories;
            CategoryContext catContext = new CategoryContext();
            var q = catContext.Categories.ToList();
            //var q = catContext.Categories.Select(reg => reg);

            //DataTable result = query.CopyToDataTable<DataRow>();

            
           
            return q;
                    
            
        }

        
    }

}