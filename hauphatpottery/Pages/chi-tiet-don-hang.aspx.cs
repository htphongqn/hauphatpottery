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

namespace hauphatpottery.Pages
{
    public partial class chi_tiet_don_hang : System.Web.UI.Page
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
        private int id = 0; int activetab = 0; int deadlineId = 0;
        private clsFormat cls = new clsFormat();
        private InventoryRepo _InventoryRepo = new InventoryRepo();
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("chi-tiet-don-hang.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            deadlineId = Utils.CIntDef(Request.QueryString["deadlineId"]);
            activetab = Utils.CIntDef(Request.QueryString["activetab"]);
            if (id > 0)
            {
                ASPxPageControl2.TabPages.FindByName("danhsachchitiet").Visible = true;
                ASPxPageControl2.TabPages.FindByName("thoigiangiaohang").Visible = true;
                ASPxPageControl2.ActiveTabIndex = activetab;
                int grouptypeId = Utils.CIntDef(Session["groupType"]);
                if (grouptypeId == Cost.GROUPTYPE_ADMIN)
                {
                    trStatus.Visible = true;
                }
                else
                {
                    trStatus.Visible = false;
                }
            }
            else
            {
                ASPxPageControl2.TabPages.FindByName("danhsachchitiet").Visible = false;
                ASPxPageControl2.TabPages.FindByName("thoigiangiaohang").Visible = false;
                trStatus.Visible = false;
            }
            if (!IsPostBack)
            {
                LoadCustomer();
                LoadProduct();
                LoadProductDetail();
                LoadProductDetailDeadline();
                LoadOrderDeadlineDetail();
                getInfo();
            }
        }
        private void LoadCustomer()
        {
            var list = _CustomerRepo.GetAll();
            ddlCustomer.DataSource = list;
            ddlCustomer.DataBind();
        }
        private void LoadProduct()
        {
            var list = _ProductRepo.GetAll();
            ddlProduct.DataSource = list;
            ddlProduct.DataBind();
        }
        private void LoadProductDetail()
        {
            int productId = Utils.CIntDef(ddlProduct.SelectedValue);
            var list = _ProductDetailRepo.GetListByProductIdAndContainsAll(productId, "");
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
        private void LoadProductDetailDeadline()
        {
            var orderDetail = _OrderDetailRepo.GetByOrderId(id);
            ddlProductDetailDeadline.Items.Clear();
            for (int i = 0; i < orderDetail.Count; i++)
            {
                int ProductDetailId = Utils.CIntDef(orderDetail[i].PRODUCT_DETAIL_ID);
                int ProductDetailSizeId = Utils.CIntDef(orderDetail[i].PRODUCT_DETAIL_SIZE_ID);
                string name = "";
                name = getProductDetailNameSize(ProductDetailId, ProductDetailSizeId);
                ddlProductDetailDeadline.Items.Add(new ListItem(name, Utils.CStrDef(orderDetail[i].ID)));
            }
            ddlProductDetailDeadline.Items.Insert(0, new ListItem("--Chọn Sản Phẩm Chi Tiết--", "0"));
        }

        #region Info & List
        private void getInfo()
        {
            try
            {
                var order = _OrderRepo.GetById(id);
                if (order != null)
                {
                    txtCode.Text = order.CODE;
                    ddlCustomer.SelectedValue = Utils.CStrDef(order.CUSTOMER_ID);
                    //pickerAndCalendarDeadlineDate.returnDate = Utils.CDateDef(order.DEADLINE_DATE, DateTime.Now);
                    pickerAndCalendarStartDate.returnDate = Utils.CDateDef(order.START_DATE, DateTime.Now);
                    txtNote.Text = Utils.CStrDef(order.NOTE);
                    rdlStatus.SelectedValue = Utils.CStrDef(order.STATUS);
                    LoadOrderDetail();
                    LoadOrderDeadline();
                }
            }
            catch
            {
            }
        }
        
        private void LoadOrderDetail()
        {
            var orderDetail = _OrderDetailRepo.GetByOrderId(id);
            ASPxGridView1_OrderDetail.DataSource = orderDetail;
            ASPxGridView1_OrderDetail.DataBind();
        }
        private void LoadOrderDeadline()
        {
            var orderDeadline = _OrderDeadlineRepo.GetByOrderId(id);
            ASPxGridViewDeadline.DataSource = orderDeadline;
            ASPxGridViewDeadline.DataBind();
        }
        private void LoadOrderDeadlineDetail()
        {
            if (deadlineId > 0)
            {
                trDeadlineDetail.Visible = true;
            }
            else
            {
                trDeadlineDetail.Visible = false;
            }
            var item = _OrderDeadlineRepo.GetById(deadlineId);
            if (item != null)
            {
                lbDeadlineDate.Text = cls.DateToStrDate(item.DEADLINE_DATE, "{0:dd/MM/yyyy}");
            }
            var orderDeadlineDetail = _OrderDeadlineOrderDetailRepo.GetListByOrderDeadlineId(deadlineId);
            if (orderDeadlineDetail != null && orderDeadlineDetail.ToList().Count > 0)
            {
                ASPxGridViewDeadlineDetail.DataSource = orderDeadlineDetail;
                ASPxGridViewDeadlineDetail.DataBind();
            }
        }
        #endregion

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductDetail();
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
                var order = _OrderRepo.GetById(id);
                if (id > 0 && order != null)
                {
                    order.CODE= txtCode.Text;
                    order.CUSTOMER_ID = Utils.CIntDef(ddlCustomer.SelectedValue);
                    order.START_DATE = pickerAndCalendarStartDate.returnDate;
                    //order.DEADLINE_DATE = pickerAndCalendarDeadlineDate.returnDate;
                    order.NOTE = txtNote.Text;
                    order.STATUS = Utils.CIntDef(rdlStatus.SelectedValue);

                    order.CREATE_DATE = DateTime.Now;
                    order.CREATOR_ID = Utils.CIntDef(Session["Userid"]);
                    
                    _OrderRepo.Update(order);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-don-hang.aspx?id=" + id : strLink;
                }
                else
                {
                    order = new ORDER();
                    order.CODE = txtCode.Text;
                    order.CUSTOMER_ID = Utils.CIntDef(ddlCustomer.SelectedValue);
                    //order.DEADLINE_DATE = pickerAndCalendarDeadlineDate.returnDate;
                    order.START_DATE = pickerAndCalendarStartDate.returnDate;
                    order.NOTE = txtNote.Text;
                    order.STATUS = Cost.DANGCHO;

                    order.CREATE_DATE = DateTime.Now;
                    order.CREATOR_ID = Utils.CIntDef(Session["Userid"]);

                    _OrderRepo.Create(order);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-don-hang.aspx?id=" + order.ID : strLink;
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
            Save("danh-sach-don-hang.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-don-hang.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _OrderRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-don-hang.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-don-hang.aspx");
        }

        protected void lbtnSaveDetail_Click(object sender, EventArgs e)
        {
            try
            {

                var orderDetail = _OrderDetailRepo.GetByProductDetailId(Utils.CIntDef(ddlProductDetail.SelectedValue), Utils.CIntDef(ddlProductDetailSize.SelectedValue), id);
                if (orderDetail != null)
                {
                    orderDetail.ORDER_ID = id;
                    orderDetail.PRODUCT_DETAIL_ID = Utils.CIntDef(ddlProductDetail.SelectedValue);
                    orderDetail.PRODUCT_DETAIL_SIZE_ID = Utils.CIntDef(ddlProductDetailSize.SelectedValue);
                    orderDetail.QUANTITY = Utils.CIntDef(txtQuantity.Text.Replace(",",""));
                    orderDetail.PRICE = Utils.CIntDef(txtPrice.Text.Replace(",", ""));
                    orderDetail.SUBTOTAL = Utils.CIntDef(txtQuantity.Text.Replace(",", "")) * Utils.CIntDef(txtPrice.Text.Replace(",", ""));
                    orderDetail.COLOR1 = ColorEdit1.Text;
                    orderDetail.COLOR2 = txtColor2.Text;
                    _OrderDetailRepo.Update(orderDetail);
                }
                else
                {
                    orderDetail = new ORDER_DETAIL();
                    orderDetail.ORDER_ID = id;
                    orderDetail.PRODUCT_DETAIL_ID = Utils.CIntDef(ddlProductDetail.SelectedValue);
                    orderDetail.PRODUCT_DETAIL_SIZE_ID = Utils.CIntDef(ddlProductDetailSize.SelectedValue);
                    orderDetail.QUANTITY = Utils.CIntDef(txtQuantity.Text.Replace(",", ""));
                    orderDetail.PRICE = Utils.CIntDef(txtPrice.Text.Replace(",", ""));
                    orderDetail.SUBTOTAL = Utils.CIntDef(txtQuantity.Text.Replace(",", "")) * Utils.CIntDef(txtPrice.Text.Replace(",", ""));
                    orderDetail.COLOR1 = ColorEdit1.Text;
                    orderDetail.COLOR2 = txtColor2.Text;
                    _OrderDetailRepo.Create(orderDetail);
                }
                Response.Redirect("chi-tiet-don-hang.aspx?id=" + id + "&activetab=" + 1);
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
                _OrderDetailRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("chi-tiet-don-hang.aspx?id=" + id + "&activetab=" + 1);
        }

        protected void lbtnSaveDeadline_Click(object sender, EventArgs e)
        {
            try
            {

                var orderDeadline = new ORDER_DEADLINE();
                orderDeadline.ORDER_ID = id;
                orderDeadline.DEADLINE_DATE = pickerAndCalendarDeadline.returnDate;
                orderDeadline.NOTE = Utils.CStrDef(txtNoteDeadline.Text);
                orderDeadline.STATUS = 0;
                _OrderDeadlineRepo.Create(orderDeadline);

                Response.Redirect("chi-tiet-don-hang.aspx?id=" + id + "&activetab=" + 2);
            }
            catch
            {
            }
        }
        
        protected void lbtnDeleteDeadline_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridViewDeadline.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                var list = _OrderDeadlineOrderDetailRepo.GetListByOrderDeadlineId(Utils.CIntDef(item));
                _OrderDeadlineOrderDetailRepo.Remove(list);

                _OrderDeadlineRepo.Remove(Utils.CIntDef(item));

            }
            Response.Redirect("chi-tiet-don-hang.aspx?id=" + id + "&activetab=" + 2);
        }
        protected void lbtnSaveDeadlineDetail_Click(object sender, EventArgs e)
        {
            try
            {
                var orderDeadlineDetail = _OrderDeadlineOrderDetailRepo.GetByOrderdeadlineIdAndOrderdetailId(deadlineId, Utils.CIntDef(ddlProductDetailDeadline.SelectedItem.Value));
                if (orderDeadlineDetail != null)
                {
                    orderDeadlineDetail.ORDER_DEADLINE_ID = deadlineId;
                    orderDeadlineDetail.ORDER_DETAIL_ID = Utils.CIntDef(ddlProductDetailDeadline.SelectedItem.Value);
                    orderDeadlineDetail.QUANTITY = Utils.CIntDef(txtQuantityDeadline.Text);
                    _OrderDeadlineOrderDetailRepo.Update(orderDeadlineDetail);
                }
                else
                {
                    orderDeadlineDetail = new ORDER_DEADLINE_ORDER_DETAIL();
                    orderDeadlineDetail.ORDER_DEADLINE_ID = deadlineId;
                    orderDeadlineDetail.ORDER_DETAIL_ID = Utils.CIntDef(ddlProductDetailDeadline.SelectedItem.Value);
                    orderDeadlineDetail.QUANTITY = Utils.CIntDef(txtQuantityDeadline.Text);
                    _OrderDeadlineOrderDetailRepo.Create(orderDeadlineDetail);
                }
                Response.Redirect("chi-tiet-don-hang.aspx?id=" + id + "&deadlineId=" + deadlineId + "&activetab=2");
            }
            catch
            {
            }
        }

        protected void lbtnDeleteDeadlineDetail_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridViewDeadline.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _OrderDeadlineOrderDetailRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("chi-tiet-don-hang.aspx?id=" + id + "&deadlineId=" + deadlineId + "&activetab=2"); 
                                ;
        }
        #endregion

        #region function
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
        public string getSumQuantity()
        {
            var orderDetail = _OrderDetailRepo.GetByOrderId(id);
            if (orderDetail != null)
            {
                return cls.FormatMoneyNotext(orderDetail.Sum(n => n.QUANTITY));
            }
            return "";
        }
        public string getSumSubtotal()
        {
            var orderDetail = _OrderDetailRepo.GetByOrderId(id);
            if (orderDetail != null)
            {
                return cls.FormatMoneyNotext(orderDetail.Sum(n => n.SUBTOTAL));
            }
            return "";
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
        public string getDeadlineStatus(object status)
        {
            return Utils.CIntDef(status) == 1 ? "Đã giao" : "Chưa giao";
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
        public string getlink_deadline(object deadlineId)
        {
            return "chi-tiet-don-hang.aspx?id=" + id + "&deadlineId=" + deadlineId + "&activetab=2";
        }
        public string getFormatQuantity(object quantity)
        {
            return cls.FormatMoneyNotext(quantity);
        }
        public int getSoluongDalam(object orderId, object productDetailId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, Cost.NHAP_THO);
            return Utils.CIntDef(list.Sum(n => n.QUANTITY));
        }
        public int getSoluongTinhDalam(object orderId, object productDetailId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, Cost.NHAP_TINH);
            return Utils.CIntDef(list.Sum(n => n.QUANTITY));
        }
        public List<INVENTORY> getListHistory(object orderId, object productDetailId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, Cost.XUAT_SANPHAM);
            return list;
        }
        public int getSoluongXuat(object orderId, object productDetailId)
        {
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            var list = _InventoryRepo.GetByOrderIdAndProductDetailId(_orderid, _prodetailid, Cost.XUAT_SANPHAM);
            return Utils.CIntDef(list.Sum(n => n.QUANTITY));
        }
        public int getSoluongConlai(object quantity, object orderId, object productDetailId)
        {
            int _quantity = Utils.CIntDef(quantity);
            int _orderid = Utils.CIntDef(orderId);
            int _prodetailid = Utils.CIntDef(productDetailId);
            var sumDalam = getSoluongXuat(_orderid, _prodetailid);
            int sumConlai = _quantity - sumDalam;
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