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
    public partial class kho_nguyen_lieu : System.Web.UI.Page
    {
        #region Declare
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();
        private MaterialRepo _MaterialRepo = new MaterialRepo();
        private clsFormat cls = new clsFormat();
        private UnitRepo _UnitRepo = new UnitRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("kho-nguyen-lieu.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadMaterial();
            }
            else
            {
                ASPxGridView1_Material.DataSource = HttpContext.Current.Session["listMaterial"];
                ASPxGridView1_Material.DataBind();
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
        public string getSLTon(object idmaterial)
        {
            int _id = Utils.CIntDef(idmaterial);
            int tongnhap = getTongSLMaterialByType(_id, Cost.NHAP_NGUYENLIEU);
            int tongxuat = getTongSLMaterialByType(_id, Cost.XUAT_NGUYENLIEU);
            int slton = tongnhap - tongxuat;
            return cls.FormatMoneyNotext(slton);
        }
        public string getTongSLNhap(object idmaterial)
        {
            int _id = Utils.CIntDef(idmaterial);
            return cls.FormatMoneyNotext(getTongSLMaterialByType(_id, Cost.NHAP_NGUYENLIEU));
        }
        public string getTongSLXuat(object idmaterial)
        {
            int _id = Utils.CIntDef(idmaterial);
            return cls.FormatMoneyNotext(getTongSLMaterialByType(_id, Cost.XUAT_NGUYENLIEU));
        }
        private int getTongSLMaterialByType(int materialId, int type)
        {
            var qty = (from a in db.ORDERMATERIAL_DETAILs
                        join b in db.ORDERMATERIALs on a.ORDERMATERIAL_ID equals b.ID
                        where a.MATERIAL_ID == materialId && b.TYPE == type
                        select a).Sum(z => z.QUANTITY);
            return Utils.CIntDef(qty);
        }
        #endregion
    }
}