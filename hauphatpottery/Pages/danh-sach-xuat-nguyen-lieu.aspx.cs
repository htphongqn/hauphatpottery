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
    public partial class danh_sach_xuat_nguyen_lieu : System.Web.UI.Page
    {
        #region Declare
        private OrderRepo _OrderRepo = new OrderRepo();
        private OrderMaterialRepo _OrderMaterialRepo = new OrderMaterialRepo();
        private UserRepo _UserRepo = new UserRepo();
        private CompanyRepo _CompanyRepo = new CompanyRepo();
        private clsFormat cls = new clsFormat();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("danh-sach-xuat-nguyen-lieu.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadOrder();
                LoadOrdermaterial();
            }
            else
            {
                ASPxGridView1_OrderMaterial.DataSource = HttpContext.Current.Session["listOrderMaterial"];
                ASPxGridView1_OrderMaterial.DataBind();
            }
        }
        private void LoadOrder()
        {
            var list = _OrderRepo.GetByStatus(Cost.SANXUAT);
            ddlOrder.DataSource = list;
            ddlOrder.DataBind();
        }
        private void LoadOrdermaterial()
        {
            try
            {
                int userId = Utils.CIntDef(Session["Userid"]);
                var list = _OrderMaterialRepo.GetByType(txtKeyword.Value, 0, Utils.CIntDef(ddlOrder.SelectedValue), Cost.XUAT_NGUYENLIEU);

                HttpContext.Current.Session["listOrderMaterial"] = list;
                ASPxGridView1_OrderMaterial.DataSource = list;
                ASPxGridView1_OrderMaterial.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadOrdermaterial();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_OrderMaterial.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _OrderMaterialRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("danh-sach-xuat-nguyen-lieu.aspx");
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
            return Utils.CIntDef(id) > 0 ? "xuat-nguyen-lieu.aspx?id=" + Utils.CIntDef(id) : "xuat-nguyen-lieu.aspx";
        }
        public string getOrderCode(object oid)
        {
            int id = Utils.CIntDef(oid);
            var item = _OrderRepo.GetById(id);
            if (item != null)
            {
                return item.CODE;
            }
            return "";
        }
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
        public string getFormatQuantity(object quantity)
        {
            return cls.FormatMoneyNotext(quantity);
        }
        public string getUser(object idUser)
        {
            int _id = Utils.CIntDef(idUser);
            var type = _UserRepo.GetById(_id);
            if (type != null)
            {
                return type.USER_NAME;
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