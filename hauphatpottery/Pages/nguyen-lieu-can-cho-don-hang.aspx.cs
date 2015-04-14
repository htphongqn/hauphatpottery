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
    public partial class nguyen_lieu_can_cho_don_hang : System.Web.UI.Page
    {
        #region Declare
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();
        private MaterialRepo _MaterialRepo = new MaterialRepo();
        private UnitRepo _UnitRepo = new UnitRepo();
        private OrderRepo _OrderRepo = new OrderRepo();
        private OrderDetailRepo _OrderDetailRepo = new OrderDetailRepo();
        private ProductDetailMaterialRepo _ProductDetailMaterialRepo = new ProductDetailMaterialRepo();
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        private int id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("nguyen-lieu-can-cho-don-hang.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadOrder();
                LoadOrder_Material();
            }
            else
            {
                ASPxGridView1_Order_Material.DataSource = HttpContext.Current.Session["listOrderMaterial"];
                ASPxGridView1_Order_Material.DataBind();
            }
        }
        private void LoadOrder()
        {
            try
            {
                var list = _OrderRepo.GetAll();
                ddlOrder.DataSource = list;
                ddlOrder.DataBind();
                ddlOrder.SelectedValue = Utils.CStrDef(id);
            }
            catch //(Exception)
            {
                //throw;
            }
        }

        private void LoadOrder_Material()
        {
            try
            {
                int OrderId = Utils.CIntDef(ddlOrder.SelectedValue);
                List<OrderMaterial> list = new List<OrderMaterial>();
                var listOrderDetail = _OrderDetailRepo.GetByOrderId(OrderId);
                foreach (var item in listOrderDetail)
                {
                    var listProductDetail_Material = _ProductDetailMaterialRepo.GetByProductDetailId(Utils.CIntDef(item.PRODUCT_DETAIL_ID));
                    foreach (var item2 in listProductDetail_Material)
                    {
                        var orderMaterial = list.Find(n => n.MATERIAL_ID == item2.MATERIAL_ID);
                        if (orderMaterial != null)
                        {
                            decimal quantity = Utils.CDecDef(item.QUANTITY) * Utils.CDecDef(item2.QUANTITY);
                            orderMaterial.QUANTITY = orderMaterial.QUANTITY + quantity;
                        }
                        else
                        {
                            orderMaterial = new OrderMaterial();
                            orderMaterial.MATERIAL_ID = Utils.CIntDef(item2.MATERIAL_ID);
                            orderMaterial.QUANTITY = Utils.CDecDef(item.QUANTITY) * Utils.CDecDef(item2.QUANTITY);
                            list.Add(orderMaterial);
                        }
                    }
                }
                HttpContext.Current.Session["listOrderMaterial"] = list;
                ASPxGridView1_Order_Material.DataSource = list;
                ASPxGridView1_Order_Material.DataBind();
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
        //public string getlink(object id)
        //{
        //    return Utils.CIntDef(id) > 0 ? "nguyen-lieu.aspx?id=" + Utils.CIntDef(id) : "nguyen-lieu.aspx";
        //}
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
        public string getNameMaterial(object materialId)
        {
            int _id = Utils.CIntDef(materialId);
            var Material = _MaterialRepo.GetById(_id);
            if (Material != null)
            {
                return Material.NAME;
            }
            return "";
        }
        public string getNameUnit(object materialId)
        {
            int _id = Utils.CIntDef(materialId);
            var Material = _MaterialRepo.GetById(_id);
            if (Material != null)
            {
                var Unit = _UnitRepo.GetById(Utils.CIntDef(Material.UNIT_ID));
                if (Unit != null)
                {
                    return Unit.NAME;
                }
            }
            return "";
        }
        #endregion

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrder_Material();
        }
    }
    public class OrderMaterial
    {
        public int MATERIAL_ID { get; set; }
        public decimal QUANTITY { get; set; }
    }
}