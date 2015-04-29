using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using vpro.functions;
using hauphatpottery.Data;
using hauphatpottery.Components;
using System.Drawing;

namespace hauphatpottery.Pages
{
    public partial class xuat_san_pham : System.Web.UI.Page
    {
        #region Declare
        private ProductRepo _ProductRepo = new ProductRepo();
        private ProductDetailRepo _ProductDetailRepo = new ProductDetailRepo();
        private ShapePropertyRepo _ShapePropertyRepo = new ShapePropertyRepo();
        private ProductDetailSizeRepo _ProductDetailSizeRepo = new ProductDetailSizeRepo();
        private TypeRepo _TypeRepo = new TypeRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private OrderRepo _OrderRepo = new OrderRepo();
        private OrderDetailRepo _OrderDetailRepo = new OrderDetailRepo();
        private OrderDeadlineRepo _OrderDeadlineRepo = new OrderDeadlineRepo();
        private OrderDeadlineOrderDetailRepo _OrderDeadlineOrderDetailRepo = new OrderDeadlineOrderDetailRepo();
        private OrderDeliRepo _OrderDeliRepo = new OrderDeliRepo();
        private OrderDeliDetailRepo _OrderDeliDetailRepo = new OrderDeliDetailRepo();
        private int id = 0; int activetab = 0;
        private clsFormat cls = new clsFormat();
        private InventoryRepo _InventoryRepo = new InventoryRepo();
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("xuat-san-pham.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            activetab = Utils.CIntDef(Request.QueryString["activetab"]);
            if (id > 0)
            {
                ASPxPageControl2.TabPages.FindByName("danhsachchitiet").Visible = true;
                ASPxPageControl2.ActiveTabIndex = activetab;
            }
            else
            {
                ASPxPageControl2.TabPages.FindByName("danhsachchitiet").Visible = false;
            }
            if (!IsPostBack)
            {
                LoadOrder();
                LoadOrderDeliDetail();
                LoadProductDetail();
                getInfo();
            }
        }
        private void LoadOrder()
        {
            var list = _OrderRepo.GetByStatus(Cost.SANXUAT);
            ddlOrder.DataSource = list;
            ddlOrder.DataBind();
        }
        private void LoadOrderDeadline()
        {
            int orderId = Utils.CIntDef(ddlOrder.SelectedValue);
            var list = _OrderDeadlineRepo.GetByOrderId(orderId);
            ddlOrderDeadline.DataSource = list;
            ddlOrderDeadline.DataBind();
            ddlOrderDeadline.Items.Insert(0, new ListItem("--Chọn Thời gian giao hàng--", "0"));
        }
        private void LoadProductDetail()
        {
            int orderId = Utils.CIntDef(ddlOrder.SelectedValue);
            var list = _ProductDetailRepo.GetListByOrderId(orderId);
            ddlProductDetail.DataSource = list;
            ddlProductDetail.DataBind();
            ddlProductDetail.Items.Insert(0, new ListItem("--Chọn Sản Phẩm Chi Tiết--", "0"));
        }
        private void LoadProductDetailSize()
        {
            int ProductDetailId = Utils.CIntDef(ddlProductDetail.SelectedValue);
            var list = _ProductDetailSizeRepo.GetByProductDetailId(ProductDetailId);
            var shapeProperty = _ShapePropertyRepo.GetByProductDetailId(ProductDetailId);
            ddlProductDetailSize.Items.Clear();
            if (list != null && list.Count > 0 && shapeProperty != null)
            {
                ddlProductDetailSize.Items.Add(new ListItem("Bộ", "-1"));
                foreach (var item in list)
                {
                    string name = "";
                    if (Utils.CIntDef(shapeProperty.D) == 1)
                        name += "D" + item.D +" ";
                    if (Utils.CIntDef(shapeProperty.H) == 1)
                        name += "H" + item.H + " ";
                    if (Utils.CIntDef(shapeProperty.L) == 1)
                        name += "L" + item.D + " "; 
                    if (Utils.CIntDef(shapeProperty.W) == 1)
                        name += "W" + item.W + " ";

                    ddlProductDetailSize.Items.Add(new ListItem(name, Utils.CStrDef(item.ID)));
                }
            }
            ddlProductDetailSize.Items.Insert(0, new ListItem("--Chọn Size--", "0"));
        }

        #region Info & List
        private void getInfo()
        {
            try
            {
                var orderDeli = _OrderDeliRepo.GetById(id);
                if (orderDeli != null)
                {
                    txtCode.Text = orderDeli.CODE;
                    ddlOrder.SelectedValue = Utils.CStrDef(orderDeli.ORDER_ID);
                    ddlOrderDeadline.SelectedValue = Utils.CStrDef(orderDeli.ORDER_DEADLINE_ID);
                    pickerAndCalendarDeliDate.returnDate = Utils.CDateDef(orderDeli.CREATE_DATE, DateTime.Now);
                    txtNote.Text = Utils.CStrDef(orderDeli.NOTE);
                }
            }
            catch
            {
            }
        }
        
        private void LoadOrderDeliDetail()
        {
            var orderDeliDetail = _OrderDeliDetailRepo.GetByOrderDeliId(id);
            ASPxGridView1_OrderDetail.DataSource = orderDeliDetail;
            ASPxGridView1_OrderDetail.DataBind();
        }

        #endregion

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrderDeadline();
            ASPxPageControl2.ActiveTabIndex = 1;
        }

        protected void ddlProductDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductDetailSize();
            ASPxPageControl2.ActiveTabIndex = 1;
        }

        #region Savedata
        private void Save(string strLink = "")
        {
            try
            {
                var orderDeli = _OrderDeliRepo.GetById(id);
                if (id > 0 && orderDeli != null)
                {
                    orderDeli.CODE = txtCode.Text;
                    orderDeli.ORDER_ID = Utils.CIntDef(ddlOrder.SelectedValue);
                    orderDeli.ORDER_DEADLINE_ID = Utils.CIntDef(ddlOrderDeadline.SelectedValue);
                    orderDeli.CREATE_DATE = pickerAndCalendarDeliDate.returnDate;
                    orderDeli.NOTE = txtNote.Text;

                    orderDeli.CREATE_DATE = DateTime.Now;
                    orderDeli.CREATOR_ID = Utils.CIntDef(Session["Userid"]);

                    _OrderDeliRepo.Update(orderDeli);

                    strLink = string.IsNullOrEmpty(strLink) ? "xuat-san-pham.aspx?id=" + id : strLink;
                }
                else
                {
                    orderDeli = new ORDER_DELI();
                    orderDeli.CODE = txtCode.Text;
                    orderDeli.ORDER_ID = Utils.CIntDef(ddlOrder.SelectedValue);
                    orderDeli.ORDER_DEADLINE_ID = Utils.CIntDef(ddlOrderDeadline.SelectedValue);
                    orderDeli.CREATE_DATE = pickerAndCalendarDeliDate.returnDate;
                    orderDeli.NOTE = txtNote.Text;

                    orderDeli.CREATE_DATE = DateTime.Now;
                    orderDeli.CREATOR_ID = Utils.CIntDef(Session["Userid"]);

                    _OrderDeliRepo.Create(orderDeli);

                    strLink = string.IsNullOrEmpty(strLink) ? "xuat-san-pham.aspx?id=" + orderDeli.ID : strLink;
                }
            }
            catch
            {
                
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
            Save("xuat-san-pham.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("xuat-san-pham.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _OrderDeliRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("xuat-san-pham.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-xuat-san-pham.aspx");
        }

        protected void lbtnSaveDetail_Click(object sender, EventArgs e)
        {
            try
            {

                var orderDeliDetail = _OrderDeliDetailRepo.GetByProductDetailId(Utils.CIntDef(ddlProductDetail.SelectedValue), Utils.CIntDef(ddlProductDetailSize.SelectedValue), id);
                if (orderDeliDetail != null)
                {
                    orderDeliDetail.ORDER_DELI_ID = id;
                    orderDeliDetail.PRODUCT_DETAIL_ID = Utils.CIntDef(ddlProductDetail.SelectedValue);
                    orderDeliDetail.PRODUCT_DETAIL_SIZE_ID = Utils.CIntDef(ddlProductDetailSize.SelectedValue);
                    orderDeliDetail.QUANTITY = Utils.CIntDef(txtQuantity.Text.Replace(",", ""));
                    _OrderDeliDetailRepo.Update(orderDeliDetail);
                }
                else
                {
                    orderDeliDetail = new ORDER_DELI_DETAIL();
                    orderDeliDetail.ORDER_DELI_ID = id;
                    orderDeliDetail.PRODUCT_DETAIL_ID = Utils.CIntDef(ddlProductDetail.SelectedValue);
                    orderDeliDetail.PRODUCT_DETAIL_SIZE_ID = Utils.CIntDef(ddlProductDetailSize.SelectedValue);
                    orderDeliDetail.QUANTITY = Utils.CIntDef(txtQuantity.Text.Replace(",", ""));
                    _OrderDeliDetailRepo.Create(orderDeliDetail);
                }
                Response.Redirect("xuat-san-pham.aspx?id=" + id + "&activetab=" + 1);
            }
            catch
            {
            }
        }

        protected void lbtnDeleteDetail_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_OrderDetail.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _OrderDeliDetailRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("xuat-san-pham.aspx?id=" + id + "&activetab=" + 1);
        }        
        
        #endregion

        #region function
        public int getSoluongDat(object productDetailId, object productDetailSizeId)
        {
            int _productDetailId = Utils.CIntDef(productDetailId);
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            var orderDeli = _OrderDeliRepo.GetById(id);
            if (orderDeli != null)
            {
                var item = _OrderDetailRepo.GetByProductDetailId(_productDetailId, _productDetailSizeId, Utils.CIntDef(orderDeli.ORDER_ID));
                return Utils.CIntDef(item.QUANTITY);
            }
            return 0;
        }
        public string getProductDetailNameSize(int productDetailId, int productDetailSizeId)
        {
            var productDetail = _ProductDetailRepo.GetById(productDetailId);
            string name = "";
            if (productDetail != null)
                name += productDetail.CODE;

            var size = _ProductDetailSizeRepo.GetById(productDetailSizeId);
            var shapeProperty = _ShapePropertyRepo.GetByProductDetailId(productDetailId);
            if (productDetailSizeId == -1)
                name += "(Bộ)";
            else
            {
                if (size != null && shapeProperty != null)
                {
                    name += "(";
                    if (Utils.CIntDef(shapeProperty.D) == 1)
                        name += "D" + size.D + " ";
                    if (Utils.CIntDef(shapeProperty.H) == 1)
                        name += "H" + size.H + " ";
                    if (Utils.CIntDef(shapeProperty.L) == 1)
                        name += "L" + size.D + " ";
                    if (Utils.CIntDef(shapeProperty.W) == 1)
                        name += "W" + size.W + " ";
                    name += ")";
                }
            }
            return name;
        }
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
        public string getProductDetailDeadline(object orderDetailId)
        {
            var orderDetail = _OrderDetailRepo.GetById(Utils.CIntDef(orderDetailId));
            if (orderDetail != null)
            {
                return getProductDetailNameSize(Utils.CIntDef(orderDetail.PRODUCT_DETAIL_ID), Utils.CIntDef(orderDetail.PRODUCT_DETAIL_SIZE_ID));
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
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
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
        public string getFormatQuantity(object quantity)
        {
            return cls.FormatMoneyNotext(quantity);
        }
        public int getSoluongKiemtra(object productDetailId, object productDetailSizeId)
        {
            int _productDetailId = Utils.CIntDef(productDetailId);
            int _productDetailSizeId = Utils.CIntDef(productDetailSizeId);
            var orderDeli = _OrderDeliRepo.GetById(id);
            if (orderDeli != null)
            {
                var list = _InventoryRepo.GetByOrderIdAndProductDetailId(Utils.CIntDef(orderDeli.ORDER_ID), _productDetailId, _productDetailSizeId, Cost.NHAP_KIEM_TINH);
                return Utils.CIntDef(list.Sum(n => n.QUANTITY));
            }
            return 0;
        }
        public int getSoluongConlai(object quantity, object productDetailId, object productDetailSizeId)
        {
            int _quantity = Utils.CIntDef(quantity);
            var sumDat = getSoluongDat(productDetailId, productDetailSizeId);
            int sumConlai = _quantity - sumDat;
            return sumConlai;
        }

        public string getProductDetailCode(object productDetailId)
        {
            var productDetail = _ProductDetailRepo.GetById(Utils.CIntDef(productDetailId));
            if (productDetail != null)
            {
                return productDetail.CODE;
            }
            return "";
        }

        public string getProductDetailName(object productDetailId)
        {
            var productDetail = _ProductDetailRepo.GetById(Utils.CIntDef(productDetailId));
            if(productDetail != null)
            {
                return productDetail.NAME;
            }
            return "";
        }
        public string getTypeName(object productDetailId)
        {
            var productDetail = _ProductDetailRepo.GetById(Utils.CIntDef(productDetailId));
            if (productDetail != null)
            {
                return productDetail.TYPE.NAME;
            }
            return "";
        }
        #endregion

    }
}