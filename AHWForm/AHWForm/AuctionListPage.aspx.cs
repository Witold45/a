﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AHWForm
{
    public partial class AuctionListPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Eception
            int s = Int32.Parse(Request.QueryString["category"]);

        }
    }
}