using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using hauphatpottery.Data;
using hauphatpottery.Components;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

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
        }
        private void LoadList()
        {
            //List<ShapePro> list = new List<ShapePro>();
            //var listShape = db.SHAPEs.ToList();
            //for (int i = 0; i < listShape.Count; i++)
            //{
            //    ShapePro item = new ShapePro();
            //    item.ID = listShape[i].ID;
            //    item.Code = listShape[i].CODE;
            //    item.Name = listShape[i].NAME;
            //    var itemPro = _ShapePropertyRepo.GetByCode(item.Code);
            //    if (itemPro != null)
            //    {
            //        item.D = Utils.CIntDef(itemPro.D);
            //        item.H = Utils.CIntDef(itemPro.H);
            //        item.L = Utils.CIntDef(itemPro.L);
            //        item.W = Utils.CIntDef(itemPro.W);
            //    }
            //    list.Add(item);
            //}
            var list = (from a in db.SHAPEs
                        join b in db.SHAPE_PROPERTies on a.CODE equals b.SHAPE_CODE
                        select new
                        {
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
        public bool getBool(object b)
        {
            return Utils.CBoolDef(b);
        }
        #endregion

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var item = _ShapeRepo.GetByCode(txtCode.Value.Trim());
                if (item != null)
                {
                    MessageBox1.ShowMessage("Mã này đã tồn tại!", "Thông báo");
                    return;
                }
                var shape = new SHAPE();
                shape.CODE = txtCode.Value;
                shape.NAME = txtName.Value;
                _ShapeRepo.Create(shape);

                var shapePro = new SHAPE_PROPERTY();
                shapePro.SHAPE_CODE = txtCode.Value;
                shapePro.D = 0;
                shapePro.H = 0;
                shapePro.L = 0;
                shapePro.W = 0;
                _ShapePropertyRepo.Create(shapePro);
            }
            catch
            {

            }
            finally
            {
                Response.Redirect("hinh-dang.aspx");
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ASPxGridView1_Shape.VisibleRowCount; i++)
            {
                ASPxCheckBox chkD = ASPxGridView1_Shape.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_Shape.Columns["D"], "chkD") as ASPxCheckBox;
                ASPxCheckBox chkH = ASPxGridView1_Shape.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_Shape.Columns["H"], "chkH") as ASPxCheckBox;
                ASPxCheckBox chkL = ASPxGridView1_Shape.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_Shape.Columns["L"], "chkL") as ASPxCheckBox;
                ASPxCheckBox chkW = ASPxGridView1_Shape.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_Shape.Columns["W"], "chkW") as ASPxCheckBox;
                HiddenField hddCode = ASPxGridView1_Shape.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_Shape.Columns["D"], "hddCode") as HiddenField;
                if (Utils.CStrDef(hddCode.Value) != "")
                {
                    var itemPro = _ShapePropertyRepo.GetByCode(hddCode.Value);
                    if (itemPro != null)
                    {
                        itemPro.D = Utils.CIntDef(chkD.Checked);
                        itemPro.H = Utils.CIntDef(chkH.Checked);
                        itemPro.L = Utils.CIntDef(chkL.Checked);
                        itemPro.W = Utils.CIntDef(chkW.Checked);
                    }
                    _ShapePropertyRepo.Update(itemPro);
                }
            }

            Response.Redirect("hinh-dang.aspx");
        }
        class ShapePro
        {
            public int ID { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public int D { get; set; }
            public int H { get; set; }
            public int L { get; set; }
            public int W { get; set; }
        }
    }
}