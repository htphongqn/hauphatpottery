using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using hauphatpottery.Data;

namespace hauphatpottery.Pages
{
    public partial class chi_tiet_khach_hang : System.Web.UI.Page
    {
        #region Declare
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("chi-tiet-khach-hang.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            if (!IsPostBack)
            {
                getInfo();
            }
        }
        #region getInfo
        private void getInfo()
        {
            try
            {
                var Customer = _CustomerRepo.GetById(id);
                if (Customer != null)
                {
                    txtName.Text = Customer.FULLNAME;
                    txtTaxCode.Text = Customer.TAX_CODE;
                    txtEmail.Text = Customer.EMAIL;
                    txtPhone.Text = Customer.PHONE;
                    txtAddress.Text = Customer.ADDRESS;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Savedata
        private void Save(string strLink = "")
        {
            try
            {
                var Customer = _CustomerRepo.GetById(id);
                if (id > 0 && Customer != null)
                {
                    Customer.FULLNAME = txtName.Text;
                    Customer.TAX_CODE = txtTaxCode.Text;
                    Customer.EMAIL = txtEmail.Text;
                    Customer.PHONE = txtPhone.Text;
                    Customer.ADDRESS = txtAddress.Text;

                    _CustomerRepo.Update(Customer);
                    
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-khach-hang.aspx?id=" + id : strLink;
                }
                else
                {
                    Customer = new CUSTOMER();
                    Customer.FULLNAME = txtName.Text;
                    Customer.TAX_CODE = txtTaxCode.Text;
                    Customer.EMAIL = txtEmail.Text;
                    Customer.PHONE = txtPhone.Text;
                    Customer.ADDRESS = txtAddress.Text;
                    Customer.CREATOR_ID = Utils.CIntDef(Session["Userid"]);
                    Customer.CREATED_DATE = DateTime.Now;

                    _CustomerRepo.Create(Customer);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-khach-hang.aspx?id=" + Customer.ID : strLink;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                if (!string.IsNullOrEmpty(strLink))
                {
                    Response.Redirect(strLink);
                }
            }
        }
        #endregion

        #region Button
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            Save("danh-sach-khach-hang.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-khach-hang.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _CustomerRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-khach-hang.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-khach-hang.aspx");
        }
        #endregion

        #region WebMethod
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchFullname(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsFullName(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.FULLNAME);
            }
            return fullnames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchPhone(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsPhone(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.PHONE);
            }
            return fullnames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchAddress(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsAddress(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.ADDRESS);
            }
            return fullnames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchEmail(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsEmail(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.EMAIL);
            }
            return fullnames;
        }
        #endregion

    }
}