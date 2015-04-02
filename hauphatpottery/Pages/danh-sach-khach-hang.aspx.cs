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
    public partial class danh_sach_khach_hang : System.Web.UI.Page
    {
        #region Declare
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private UserRepo _UserRepo = new UserRepo();
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("danh-sach-khach-hang.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            if (!IsPostBack)
            {
                LoadCustomer();
            }
            else
            {
                ASPxGridView1_Customer.DataSource = HttpContext.Current.Session["listCustomer"];
                ASPxGridView1_Customer.DataBind();
            }
        }

        private void LoadCustomer()
        {
            try
            {
                var list = _CustomerRepo.GetListByContainsAll(txtKeyword.Value);

                HttpContext.Current.Session["listCustomer"] = list;
                ASPxGridView1_Customer.DataSource = list;
                ASPxGridView1_Customer.DataBind();

            }
            catch //(Exception)
            {

                //throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Customer.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _CustomerRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("danh-sach-khach-hang.aspx");
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
            return Utils.CIntDef(id) > 0 ? "chi-tiet-khach-hang.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-khach-hang.aspx";
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
        #endregion
    }
}