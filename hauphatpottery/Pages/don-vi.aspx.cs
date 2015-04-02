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
    public partial class don_vi : System.Web.UI.Page
    {
        #region Declare
        private UnitRepo _UnitRepo = new UnitRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("don-vi.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadInfo();
                LoadUnit();
            }
            else
            {
                ASPxGridView1_Unit.DataSource = HttpContext.Current.Session["listUnit"];
                ASPxGridView1_Unit.DataBind();
            }
        }
        private void LoadUnit()
        {
            try
            {
                var list = _UnitRepo.GetAll();

                HttpContext.Current.Session["listUnit"] = list;
                ASPxGridView1_Unit.DataSource = list;
                ASPxGridView1_Unit.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Unit.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _UnitRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("don-vi.aspx");
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
            return Utils.CIntDef(id) > 0 ? "don-vi.aspx?id=" + Utils.CIntDef(id) : "don-vi.aspx";
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
            var Type = _UnitRepo.GetById(id);
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
                var Unit = _UnitRepo.GetById(id);
                if (id > 0 && Unit != null)
                {
                    Unit.NAME = txtName.Value;
                    Unit.NOTE = txtNote.Value;

                    _UnitRepo.Update(Unit);
                }
                else
                {
                    Unit = new UNIT();
                    Unit.NAME = txtName.Value;
                    Unit.NOTE = txtNote.Value;

                    _UnitRepo.Create(Unit);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Response.Redirect("don-vi.aspx");
            }
        }
    }
}