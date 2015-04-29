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
    public partial class nhap_san_pham_tinh_chet : System.Web.UI.Page
    {
        #region Declare
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();
        private MaterialRepo _MaterialRepo = new MaterialRepo();
        private UnitRepo _UnitRepo = new UnitRepo();
        private OrderRepo _OrderRepo = new OrderRepo();
        private ShapePropertyRepo _ShapePropertyRepo = new ShapePropertyRepo();
        private ProductDetailSizeRepo _ProductDetailSizeRepo = new ProductDetailSizeRepo();
        private OrderDetailRepo _OrderDetailRepo = new OrderDetailRepo();
        private ProductDetailMaterialRepo _ProductDetailMaterialRepo = new ProductDetailMaterialRepo();
        private ProductDetailRepo _ProductDetailRepo = new ProductDetailRepo();
        private InventoryRepo _InventoryRepo = new InventoryRepo();
        private clsFormat cls = new clsFormat();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("nhap-san-pham-tinh-chet.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadOrder();
                LoadProductDetail();
                LoadProductDetailSize();
                LoadOrder_ProductDetail();
            }
            else
            {
                ASPxGridView1_Order_ProductDetail.DataSource = HttpContext.Current.Session["listOrderProductDetail"];
                ASPxGridView1_Order_ProductDetail.DataBind();
            }
        }
        private void LoadOrder()
        {
            try
            {
                var list = _OrderRepo.GetByStatus(Cost.SANXUAT);
                ddlOrder.DataSource = list;
                ddlOrder.DataBind();
                ddlOrder.SelectedValue = Utils.CStrDef(id);
            }
            catch //(Exception)
            {
                //throw;
            }
        }
        private void LoadProductDetail()
        {
            try
            {
                int OrderId = Utils.CIntDef(ddlOrder.SelectedValue);
                var list = _ProductDetailRepo.GetListByOrderId(OrderId);
                ddlProductDetail.DataSource = list;
                ddlProductDetail.DataBind();
                ddlProductDetail.Items.Insert(0, new ListItem("--Chọn Sản Phẩm Chi Tiết--", "0"));
            }
            catch //(Exception)
            {
                //throw;
            }
        }
        private void LoadProductDetailSize()
        {
            int OrderId = Utils.CIntDef(ddlOrder.SelectedValue);
            int ProductDetailId = Utils.CIntDef(ddlProductDetail.SelectedValue);
            var list = _OrderDetailRepo.GetByProductDetailId(ProductDetailId, OrderId);
            var shapeProperty = _ShapePropertyRepo.GetByProductDetailId(ProductDetailId);
            ddlProductDetailSize.Items.Clear();
            if (list != null && list.Count > 0 && shapeProperty != null)
            {
                foreach (var item in list)
                {
                    int productDetailSizeId = Utils.CIntDef(item.PRODUCT_DETAIL_SIZE_ID);
                    if (productDetailSizeId == -1)
                    {
                        ddlProductDetailSize.Items.Add(new ListItem("Bộ", "-1"));
                    }
                    else
                    {
                        var itemSize = _ProductDetailSizeRepo.GetById(productDetailSizeId);
                        if (itemSize != null)
                        {
                            string name = "";
                            if (Utils.CIntDef(shapeProperty.D) == 1)
                                name += "D" + itemSize.D + " ";
                            if (Utils.CIntDef(shapeProperty.H) == 1)
                                name += "H" + itemSize.H + " ";
                            if (Utils.CIntDef(shapeProperty.L) == 1)
                                name += "L" + itemSize.D + " ";
                            if (Utils.CIntDef(shapeProperty.W) == 1)
                                name += "W" + itemSize.W + " ";

                            ddlProductDetailSize.Items.Add(new ListItem(name, Utils.CStrDef(productDetailSizeId)));
                        }
                    }
                }
            }
            ddlProductDetailSize.Items.Insert(0, new ListItem("--Chọn Size--", "0"));
        }
        private void LoadOrder_ProductDetail()
        {
            try
            {
                int OrderId = Utils.CIntDef(ddlOrder.SelectedValue);
                var listProductDetail = _OrderDetailRepo.GetByOrderId(OrderId);
                HttpContext.Current.Session["listOrderProductDetail"] = listProductDetail;
                ASPxGridView1_Order_ProductDetail.DataSource = listProductDetail;
                ASPxGridView1_Order_ProductDetail.DataBind();
            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductDetail();
            LoadOrder_ProductDetail();
        }
        protected void ddlProductDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductDetailSize();
        }
        #region Function
        public string getProductDetailSize(object productDetailId, object productDetailSizeId)
        {
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            if (_productDetailSizeId == -1)
                return "Bộ";

            var item = _ProductDetailSizeRepo.GetById(_productDetailSizeId);
            var shapeProperty = _ShapePropertyRepo.GetByProductDetailId(Utils.CIntDef(productDetailId));

            if (item != null && shapeProperty != null)
            {
                string name = "";
                if (Utils.CIntDef(shapeProperty.D) == 1)
                    name += "D" + item.D + " ";
                if (Utils.CIntDef(shapeProperty.H) == 1)
                    name += "H" + item.H + " ";
                if (Utils.CIntDef(shapeProperty.L) == 1)
                    name += "L" + item.D + " ";
                if (Utils.CIntDef(shapeProperty.W) == 1)
                    name += "W" + item.W + " ";
                return name;
            }
            return "";
        }
        public int getProductIdByProductDetailId(object productDetailId)
        {
            int _id = Utils.CIntDef(productDetailId);
            var ProductDetail = _ProductDetailRepo.GetById(_id);
            if (ProductDetail != null)
            {
                return Utils.CIntDef(ProductDetail.PRODUCT_ID);
            }
            return 0;
        }
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
        public string getFormatQuantity(object quantity)
        {
            return cls.FormatMoneyNotext(quantity);
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
        public string getNameUnit(object materialId)
        {
            int _id = Utils.CIntDef(materialId);
            var Material = _MaterialRepo.GetById(_id);
            if (Material != null)
            {
                return Material.UNIT.NAME;
            }
            return "";
        }
        public string getCodeProductDetail(object oid)
        {
            int _id = Utils.CIntDef(oid);
            var item = _ProductDetailRepo.GetById(_id);
            if (item != null)
            {
                return item.CODE;
            }
            return "";
        }
        public string getProductDetailName(object oid)
        {
            int _id = Utils.CIntDef(oid);
            var item = _ProductDetailRepo.GetById(_id);
            if (item != null)
            {
                return item.NAME;
            }
            return "";
        }
        public int getSoluongDalam(object orderId, object productDetailId, object productDetailSizeId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, _productDetailSizeId, Cost.NHAP_THO);
            return Utils.CIntDef(list.Sum(n => n.QUANTITY));
        }
        public int getSoluongTinhSonDalam(object orderId, object productDetailId, object productDetailSizeId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, _productDetailSizeId, Cost.NHAP_TINH_SON);
            return Utils.CIntDef(list.Sum(n => n.QUANTITY));
        }
        public List<INVENTORY> getListHistory(object orderId, object productDetailId, object productDetailSizeId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, _productDetailSizeId, Cost.NHAP_TINH_CHET);
            return list;
        }
        public int getSoluongTinhChetDalam(object orderId, object productDetailId, object productDetailSizeId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, _productDetailSizeId, Cost.NHAP_TINH_CHET);
            return Utils.CIntDef(list.Sum(n => n.QUANTITY));
        }
        public int getSoluongConlai(object quantity, object orderId, object productDetailId, object productDetailSizeId)
        {
            int _quantity = Utils.CIntDef(quantity);
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            var sumDalam = getSoluongTinhChetDalam(_orderid, _prodetailid, _productDetailSizeId);
            int sumConlai = _quantity - sumDalam;
            return sumConlai;
        }
        #endregion

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int productDetailId = Utils.CIntDef(ddlProductDetail.SelectedValue);
                int orderId = Utils.CIntDef(ddlOrder.SelectedValue);
                int productDetailSizeId = Utils.CIntDef(ddlProductDetailSize.SelectedValue);

                var item = new INVENTORY();
                item.PRODUCT_DETAIL_ID = productDetailId;
                item.ORDER_ID = orderId;
                item.PRODUCT_DETAIL_SIZE_ID = productDetailSizeId;
                item.QUANTITY = Utils.CIntDef(txtQuantity.Value.Replace(",", ""));
                item.CREATE_DATE = DateTime.Now;
                item.TYPE = Cost.NHAP_TINH_CHET;
                item.CREATOR_ID = Utils.CIntDef(Session["Userid"]);

                _InventoryRepo.Create(item);

                Response.Redirect("nhap-san-pham-tinh-chet.aspx?id=" + item.ORDER_ID);

            }
            catch
            {
            }
        }

    }
}