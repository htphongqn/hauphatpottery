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
    public partial class loai_hang : System.Web.UI.Page
    {
        #region Declare
        private TypeRepo _TypeRepo = new TypeRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("loai-hang.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadInfo();
                LoadType();
            }
            else
            {
                ASPxGridView1_Type.DataSource = HttpContext.Current.Session["listType"];
                ASPxGridView1_Type.DataBind();
            }
        }
        private void LoadType()
        {
            try
            {
                var list = _TypeRepo.GetAll();

                HttpContext.Current.Session["listType"] = list;
                ASPxGridView1_Type.DataSource = list;
                ASPxGridView1_Type.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Type.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _TypeRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("loai-hang.aspx");
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
            return Utils.CIntDef(id) > 0 ? "loai-hang.aspx?id=" + Utils.CIntDef(id) : "loai-hang.aspx";
        }
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
        #endregion

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void LoadInfo()
        {
            var Type = _TypeRepo.GetById(id);
            if (Type != null)
            {
                txtName.Value = Type.NAME;
                txtNote.Value = Type.NOTE;
            }
        }
        private void Save()
        {
            try
            {
                var Type = _TypeRepo.GetById(id);
                if (id > 0 && Type != null)
                {
                    Type.NAME = txtName.Value;
                    Type.NOTE = txtNote.Value;

                    _TypeRepo.Update(Type);
                }
                else
                {
                    Type = new TYPE();
                    Type.NAME = txtName.Value;
                    Type.NOTE = txtNote.Value;

                    _TypeRepo.Create(Type);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Response.Redirect("loai-hang.aspx");
            }
        }
    }
}