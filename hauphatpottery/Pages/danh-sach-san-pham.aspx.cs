using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using hauphatpottery.Data;
using hauphatpottery.Components;

namespace hauphatpottery.Pages
{
    public partial class danh_sach_san_pham : System.Web.UI.Page
    {
        #region Declare
        private ProductRepo _ProductRepo = new ProductRepo();
        private UserRepo _UserRepo = new UserRepo();
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("danh-sach-san-pham.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            if (!IsPostBack)
            {
                LoadProduct();
            }
            else
            {
                ASPxGridView1_Product.DataSource = HttpContext.Current.Session["listProduct"];
                ASPxGridView1_Product.DataBind();
            }
        }

        private void LoadProduct()
        {
            try
            {
                var list = _ProductRepo.GetListByContainsCode(txtKeyword.Value);

                HttpContext.Current.Session["listProduct"] = list;
                ASPxGridView1_Product.DataSource = list;
                ASPxGridView1_Product.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Product.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _ProductRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("danh-sach-san-pham.aspx");
        }

        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        #region Function
        public string getShortString(object title, int length)
        {
            string str = Utils.CStrDef(title);
            if (str.Length > length)
                str = str.Substring(0, length - 3) + "...";
            return str;
        }
        public string getlink(object id)
        {
            return Utils.CIntDef(id) > 0 ? "chi-tiet-san-pham.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-san-pham.aspx";
        }
        public string getlinkdetail(object id)
        {
            return Utils.CIntDef(id) > 0 ? "danh-sach-san-pham-chi-tiet.aspx?id=" + Utils.CIntDef(id) : "danh-sach-san-pham-chi-tiet.aspx";
        }
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
        public string getUserName(object idUser)
        {
            int _id = Utils.CIntDef(idUser);
            var user = _UserRepo.GetById(_id);
            if (user != null)
            {
                return user.USER_NAME;
            }
            return "";
        }
        public string getImage(object title)
        {
            string str = Utils.CStrDef(title);
            str = Cost.PRODUCTPATH + str;
            return str;
        }
        #endregion
    }
}