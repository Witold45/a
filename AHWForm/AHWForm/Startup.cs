using System;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Web.Caching;
using AHWForm.Models;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using AHWForm.Classes_And_Interfaces;

[assembly: OwinStartupAttribute(typeof(AHWForm.Startup))]
namespace AHWForm
{
    public partial class Startup: IExtensionMethods
        { 
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            AddTask("RefreshAuctions", 1);
        }

        private static CacheItemRemovedCallback OnCacheRemove = null;

        protected void Application_Start(object sender, EventArgs e)
        {
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
            if(k == "RefreshAuctions") {


                ExtensionMethods.RefreshDB();
            
            AddTask(k, Convert.ToInt32(v));
            }
        }
    }
}
