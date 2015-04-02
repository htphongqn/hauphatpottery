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
    public partial class nguyen_lieu : System.Web.UI.Page
    {
        #region Declare
        private MaterialRepo _MaterialRepo = new MaterialRepo();
        private UnitRepo _UnitRepo = new UnitRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("nguyen-lieu.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadUnit();
                LoadInfo();
                LoadMaterial();
            }
            else
            {
                ASPxGridView1_Material.DataSource = HttpContext.Current.Session["listMaterial"];
                ASPxGridView1_Material.DataBind();
            }
        }
        private void LoadUnit()
        {
            try
            {
                var list = _UnitRepo.GetAll();
                ddlUnit.DataSource = list;
                ddlUnit.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }
        private void LoadMaterial()
        {
            try
            {
                var list = _MaterialRepo.GetAll();

                HttpContext.Current.Session["listMaterial"] = list;
                ASPxGridView1_Material.DataSource = list;
                ASPxGridView1_Material.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Material.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _MaterialRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("nguyen-lieu.aspx");
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
            return Utils.CIntDef(id) > 0 ? "nguyen-lieu.aspx?id=" + Utils.CIntDef(id) : "nguyen-lieu.aspx";
        }
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
        public string getUnit(object idUnit)
        {
            int _id = Utils.CIntDef(idUnit);
            var unit = _UnitRepo.GetById(_id);
            if (unit != null)
            {
                return unit.NAME;
            }
            return "";
        }
        #endregion

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void LoadInfo()
        {
            var Material = _MaterialRepo.GetById(id);
            if (Material != null)
            {
                txtName.Value = Material.NAME;
                txtNote.Value = Material.NOTE;
                ddlUnit.SelectedValue = Utils.CStrDef(Material.UNIT_ID);
            }
        }
        private void Save()
        {
            try
            {
                var Material = _MaterialRepo.GetById(id);
                if (id > 0 && Material != null)
                {
                    Material.NAME = txtName.Value;
                    Material.NOTE = txtNote.Value;
                    Material.UNIT_ID = Utils.CIntDef(ddlUnit.SelectedValue);

                    _MaterialRepo.Update(Material);
                }
                else
                {
                    Material = new MATERIAL();
                    Material.NAME = txtName.Value;
                    Material.NOTE = txtNote.Value;
                    Material.UNIT_ID = Utils.CIntDef(ddlUnit.SelectedValue);

                    _MaterialRepo.Create(Material);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Response.Redirect("nguyen-lieu.aspx");
            }
        }
    }
}