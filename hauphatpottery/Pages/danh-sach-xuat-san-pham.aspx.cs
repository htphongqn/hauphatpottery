using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using hauphatpottery.Data;
using hauphatpottery.Components;
using System.Drawing;

namespace hauphatpottery.Pages
{
    public partial class danh_sach_xuat_san_pham : System.Web.UI.Page
    {
        #region Declare
        private OrderRepo _OrderRepo = new OrderRepo();
        private OrderDeadlineRepo _OrderDeadlineRepo = new OrderDeadlineRepo();
        private UserRepo _UserRepo = new UserRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private OrderDeliRepo _OrderDeliRepo = new OrderDeliRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("danh-sach-xuat-san-pham.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                pickerAndCalendarFrom.returnDate = DateTime.Now.AddMonths(-2);
                pickerAndCalendarTo.returnDate = DateTime.Now;
                LoadOrder();
                LoadList();
            }
            else
            {
                ASPxGridView1_Order.DataSource = HttpContext.Current.Session["listOrder"];
                ASPxGridView1_Order.DataBind();
            }
        }
        private void LoadOrder()
        {
            var list = _OrderRepo.GetAll();
            ddlOrder.DataSource = list;
            ddlOrder.DataBind();
        }
        private void LoadList()
        {
            try
            {
                int userId = Utils.CIntDef(Session["Userid"]);
                var list = _OrderDeliRepo.GetByWhereAll(txtKeyword.Value, Utils.CIntDef(ddlOrder.SelectedValue), 0, pickerAndCalendarFrom.returnDate, pickerAndCalendarTo.returnDate);

                HttpContext.Current.Session["LoadOrder"] = list;
                ASPxGridView1_Order.DataSource = list;
                ASPxGridView1_Order.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadOrder();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Order.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _OrderDeliRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("danh-sach-xuat-san-pham.aspx");
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
            return Utils.CIntDef(id) > 0 ? "xuat-san-pham.aspx?id=" + Utils.CIntDef(id) : "xuat-san-pham.aspx";
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
        public DateTime getOrderDeadline(object oid)
        {
            int id = Utils.CIntDef(oid);
            var item = _OrderDeadlineRepo.GetById(id);
            if (item != null)
            {
                return Utils.CDateDef(item.DEADLINE_DATE, DateTime.Now);
            }
            return DateTime.Now;
        }
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
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