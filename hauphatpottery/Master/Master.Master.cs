﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hauphatpottery.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Userid"]==null)
            {
                Response.Redirect("/Pages/dang-nhap.aspx", false);
            }
        }
    }
}