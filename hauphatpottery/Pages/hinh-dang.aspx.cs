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
    public partial class hinh_dang : System.Web.UI.Page
    {
        #region Declare
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();
        private ShapeRepo _ShapeRepo = new ShapeRepo();
        private ShapePropertyRepo _ShapePropertyRepo = new ShapePropertyRepo();
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("hinh-dang.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            if (!IsPostBack)
            {
                LoadList();
            }
            else
            {
                ASPxGridView1_Shape.DataSource = HttpContext.Current.Session["listShape"];
                ASPxGridView1_Shape.DataBind();
            }
        }
        private void LoadList()
        {
            var list = (from a in db.SHAPEs 
                       join b in db.SHAPE_PROPERTies on a.CODE equals b.SHAPE_CODE
                       select new {
                           a.ID
                           ,a.CODE
                           ,a.NAME
                           ,b.D
                           ,b.H
                           ,b.L
                           ,b.W
                       }).ToList();
            HttpContext.Current.Session["listShape"] = list;
            ASPxGridView1_Shape.DataSource = list;
            ASPxGridView1_Shape.DataBind();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Shape.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _ShapeRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("hinh-dang.aspx");
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
            return Utils.CIntDef(id) > 0 ? "hinh-dang.aspx?id=" + Utils.CIntDef(id) : "hinh-dang.aspx";
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

        private void Save()
        {
            try
            {
                var item = _ShapeRepo.GetByCode(txtCode.Value.Trim());
                if (item != null)
                {
                    MessageBox1.ShowMessage("Mã này đã tồn tại!","Thông báo");
                    return;
                }
                var shape = new SHAPE();
                shape.CODE = txtCode.Value;
                shape.NAME = txtName.Value;
                _ShapeRepo.Create(shape);
            }
            catch
            {
                
            }
            finally
            {
                Response.Redirect("hinh-dang.aspx");
            }
        }
    }
}