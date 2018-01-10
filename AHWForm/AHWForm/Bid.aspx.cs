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
    public partial class Bid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void BidSecond_Click(object sender, EventArgs e)
        {
            string Id = Request.QueryString["Id"];
            if (Id != null)
            {
                BidContext bidContext= new BidContext();
                //BidsModel temp = new BidsModel();

                BidsModel bidsModel = new BidsModel()
                {
                    AuctionId = Id,
                    UserId = HttpContext.Current.User.Identity.GetUserId(),
                    Id = Guid.NewGuid().ToString(),
                    Value = Decimal.Parse(price.Text),
                };

                bidContext.Bids.Add(bidsModel);
                bidContext.SaveChanges();


            }
        }

        
    }
}