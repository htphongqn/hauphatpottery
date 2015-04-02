using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using hauphatpottery.Data;
using hauphatpottery.Components;

namespace hauphatpottery.ToolTip
{
    public partial class ToolTip : System.Web.UI.Page
    {
        #region Declare
        private ProductRepo _ProductRepo = new ProductRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Load_tooltip();
        }
        private void Load_tooltip()
        {

            string str = string.Empty;
            int oid =Utils.CIntDef(Request.QueryString["oid"]);
            var item = _ProductRepo.GetById(oid);
            if (item != null)
            {
                str += "<div><img src='" + getImage(item.IMAGE) + "' width='140'/></div>";
            }
            
            ltrInfo.Text = str;
        }
        public string getImage(object title)
        {
            string str = Utils.CStrDef(title);
            str = Cost.PRODUCTPATH + str;
            return str;
        }
    }
}