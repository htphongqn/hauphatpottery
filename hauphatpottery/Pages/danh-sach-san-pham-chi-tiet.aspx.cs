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
    public partial class danh_sach_san_pham_chi_tiet : System.Web.UI.Page
    {
        #region Declare
        private ProductRepo _ProductRepo = new ProductRepo();
        private ProductDetailRepo _ProductDetailRepo = new ProductDetailRepo();
        private TypeRepo _TypeRepo = new TypeRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("danh-sach-san-pham-chi-tiet.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadProduct();
                LoadProductDetail();
            }
            else
            {
                ASPxGridView1_ProductDetail.DataSource = HttpContext.Current.Session["listProductDetail"];
                ASPxGridView1_ProductDetail.DataBind();
            }
        }
        private void LoadProduct()
        {
            var list = _ProductRepo.GetAll();
            ddlProduct.DataSource = list;
            ddlProduct.DataBind();
            ddlProduct.SelectedValue = id.ToString();
        }
        private void LoadProductDetail()
        {
            try
            {
                var list = _ProductDetailRepo.GetListByProductIdAndContainsAll(Utils.CIntDef(ddlProduct.SelectedValue), txtKeyword.Value);

                HttpContext.Current.Session["listProductDetail"] = list;
                ASPxGridView1_ProductDetail.DataSource = list;
                ASPxGridView1_ProductDetail.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadProductDetail();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_ProductDetail.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _ProductDetailRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("danh-sach-san-pham-chi-tiet.aspx");
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
            return Utils.CIntDef(id) > 0 ? "chi-tiet-san-pham-chi-tiet.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-san-pham-chi-tiet.aspx";
        }
        public string getlink_nguyenlieucan(object id)
        {
            return Utils.CIntDef(id) > 0 ? "nguyen-lieu-can-cho-san-pham.aspx?id=" + Utils.CIntDef(id) : "nguyen-lieu-can-cho-san-pham.aspx";
        }
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
        public string getType(object idType)
        {
            int _id = Utils.CIntDef(idType);
            var type = _TypeRepo.GetById(_id);
            if (type != null)
            {
                return type.NAME;
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