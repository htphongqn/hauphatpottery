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
    public partial class chi_tiet_cong_ty : System.Web.UI.Page
    {
        #region Declare
        private CompanyRepo _CompanyRepo = new CompanyRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("chi-tiet-cong-ty.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
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
                var Company = _CompanyRepo.GetById(id);
                if (Company != null)
                {
                    txtName.Text = Company.NAME;
                    //txtTaxCode.Text = Customer.TAX_CODE;
                    txtEmail.Text = Company.EMAIL;
                    txtPhone.Text = Company.PHONE;
                    txtAddress.Text = Company.ADDRESS;
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
                var Company = _CompanyRepo.GetById(id);
                if (id > 0 && Company != null)
                {
                    Company.NAME = txtName.Text;
                    //Company.TAX_CODE = txtTaxCode.Text;
                    Company.EMAIL = txtEmail.Text;
                    Company.PHONE = txtPhone.Text;
                    Company.ADDRESS = txtAddress.Text;

                    _CompanyRepo.Update(Company);
                    
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-cong-ty.aspx?id=" + id : strLink;
                }
                else
                {
                    Company = new COMPANY();
                    Company.NAME = txtName.Text;
                    //Customer.TAX_CODE = txtTaxCode.Text;
                    Company.EMAIL = txtEmail.Text;
                    Company.PHONE = txtPhone.Text;
                    Company.ADDRESS = txtAddress.Text;
                    Company.CREATOR_ID = Utils.CIntDef(Session["Userid"]);
                    Company.CREATED_DATE = DateTime.Now;

                    _CompanyRepo.Create(Company);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-cong-ty.aspx?id=" + Company.ID : strLink;
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
            Save("danh-sach-cong-ty.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-cong-ty.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _CompanyRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-cong-ty.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-cong-ty.aspx");
        }
        #endregion

        #region WebMethod
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchFullname(string prefixText, int count)
        {
            var list = new CompanyRepo().GetListByContainsName(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.NAME);
            }
            return fullnames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchPhone(string prefixText, int count)
        {
            var list = new CompanyRepo().GetListByContainsPhone(prefixText);
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
            var list = new CompanyRepo().GetListByContainsAddress(prefixText);
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
            var list = new CompanyRepo().GetListByContainsEmail(prefixText);
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