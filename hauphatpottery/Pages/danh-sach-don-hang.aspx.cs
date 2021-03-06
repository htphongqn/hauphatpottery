﻿using System;
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
    public partial class danh_sach_don_hang : System.Web.UI.Page
    {
        #region Declare
        private OrderRepo _OrderRepo = new OrderRepo();
        private OrderDeadlineRepo _OrderDeadlineRepo = new OrderDeadlineRepo();
        private UserRepo _UserRepo = new UserRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("danh-sach-don-hang.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                pickerAndCalendarFrom.returnDate = DateTime.Now.AddMonths(-2);
                pickerAndCalendarTo.returnDate = DateTime.Now;
                LoadCustomer();
                LoadOrder();
            }
            else
            {
                ASPxGridView1_Order.DataSource = HttpContext.Current.Session["listOrder"];
                ASPxGridView1_Order.DataBind();
            }
        }
        private void LoadCustomer()
        {
            var list = _CustomerRepo.GetAll();
            ddlCustomer.DataSource = list;
            ddlCustomer.DataBind();
        }
        private void LoadOrder()
        {
            try
            {
                int userId = Utils.CIntDef(Session["Userid"]);
                var list = _OrderRepo.GetByWhereAll(txtKeyword.Value, Utils.CIntDef(ddlCustomer.SelectedValue), Utils.CIntDef(ddlStatus.SelectedItem.Value), userId, pickerAndCalendarFrom.returnDate, pickerAndCalendarTo.returnDate);
                int grouptypeId = Utils.CIntDef(Session["groupType"]);
                if (grouptypeId == Cost.GROUPTYPE_ADMIN)
                {
                    list = _OrderRepo.GetByWhereAll(txtKeyword.Value, Utils.CIntDef(ddlCustomer.SelectedValue), Utils.CIntDef(ddlStatus.SelectedItem.Value), 0, pickerAndCalendarFrom.returnDate, pickerAndCalendarTo.returnDate);
                }

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
                _OrderRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("danh-sach-don-hang.aspx");
        }

        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }
        protected void ASPxGridView1_Order_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            var c = _OrderRepo.GetById(Utils.CIntDef(e.KeyValue));
            if (c != null && c.STATUS == 2)
            {

                var listDeadline = _OrderDeadlineRepo.GetByOrderId(Utils.CIntDef(e.KeyValue)).Where(n => n.STATUS != 1).OrderBy(n => n.ID);
                if (listDeadline != null && listDeadline.ToList().Count > 0 && listDeadline.ToList()[0].ISCHECK != 1)
                {
                    DateTime dateDeadline = Utils.CDateDef(listDeadline.ToList()[0].DEADLINE_DATE, DateTime.MinValue);
                    int d = (dateDeadline.Date - DateTime.Now.Date).Days;
                    if (d < 0)
                    {
                        e.Row.ForeColor = Color.White;
                        e.Row.BackColor = Color.Red;                        
                    }
                    else if (d <= 10)
                    {
                        e.Row.ForeColor = Color.Red;
                        e.Row.BackColor = Color.Yellow;
                    }
                }

            }
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
            return Utils.CIntDef(id) > 0 ? "chi-tiet-don-hang.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-don-hang.aspx";
        }
        public string getlink_nguyenlieucan(object id)
        {
            return Utils.CIntDef(id) > 0 ? "nguyen-lieu-can-cho-don-hang.aspx?id=" + Utils.CIntDef(id) : "nguyen-lieu-can-cho-don-hang.aspx";
        }
        public string getlinkCus(object id)
        {
            return Utils.CIntDef(id) > 0 ? "chi-tiet-khach-hang.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-khach-hang.aspx";
        }
        public string getNameCus(object oid)
        {
            int id = Utils.CIntDef(oid);
            var item = _CustomerRepo.GetById(id);
            if (item != null)
            {
                return item.FULLNAME;
            }
            return "";
        }
        public string getStatusName(object oid)
        {
            int id = Utils.CIntDef(oid);
            switch (id)
            {
                case 1:
                    return "Đang chờ";
                case 2:
                    return "Sản xuất";
                case 3:
                    return "Hoàn tất";
                default:
                    return "";
            }
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