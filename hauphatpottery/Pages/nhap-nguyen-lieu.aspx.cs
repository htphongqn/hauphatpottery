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
    public partial class nhap_nguyen_lieu : System.Web.UI.Page
    {
        #region Declare
        private MaterialRepo _MaterialRepo = new MaterialRepo();
        private TypeRepo _TypeRepo = new TypeRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private CompanyRepo _CompanyRepo = new CompanyRepo();
        private OrderMaterialPriceRepo _OrderMaterialPriceRepo = new OrderMaterialPriceRepo();
        private OrderMaterialRepo _OrderMaterialRepo = new OrderMaterialRepo();
        private OrderMaterialDetailRepo _OrderMaterialDetailRepo = new OrderMaterialDetailRepo();
        private int id = 0; int activetab = 0;
        private clsFormat cls = new clsFormat();
        private UnitRepo _UnitRepo = new UnitRepo();
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("nhap-nguyen-lieu.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            activetab = Utils.CIntDef(Request.QueryString["activetab"]);
            if (id > 0)
            {
                ASPxPageControl2.TabPages.FindByName("danhsachchitiet").Visible = true;
                ASPxPageControl2.TabPages.FindByName("lichsutra").Visible = true;
                ASPxPageControl2.ActiveTabIndex = activetab;
            }
            else
            {
                ASPxPageControl2.TabPages.FindByName("danhsachchitiet").Visible = false;
                ASPxPageControl2.TabPages.FindByName("lichsutra").Visible = false;
            }
            if (!IsPostBack)
            {
                LoadCompany();
                LoadMaterial();
                getInfo();
                LoadOrderMaterialDetail();
                LoadOrderMaterialPrice();
            }
        }
        private void LoadCompany()
        {
            var list = _CompanyRepo.GetAll();
            ddlCompany.DataSource = list;
            ddlCompany.DataBind();
        }
        private void LoadMaterial()
        {
            var list = _MaterialRepo.GetAll();
            ddlMaterial.DataSource = list;
            ddlMaterial.DataBind();
        }

        #region Info & List
        private void getInfo()
        {
            try
            {
                var orderMaterial = _OrderMaterialRepo.GetById(id);
                if (orderMaterial != null)
                {
                    txtCode.Text = orderMaterial.CODE;
                    txtOrderPrice.Text = getFormatQuantity(orderMaterial.ORDERPRICE);
                    ddlCompany.SelectedValue = Utils.CStrDef(orderMaterial.COMPANY_ID);
                    pickerAndCalendarDate.returnDate = Utils.CDateDef(orderMaterial.CREATED_DATE, DateTime.Now);
                    txtNote.Text = Utils.CStrDef(orderMaterial.NOTE);
                }
            }
            catch
            {
            }
        }
        
        private void LoadOrderMaterialDetail()
        {
            var orderMaterialDetail = _OrderMaterialDetailRepo.GetByOrderMaterialId(id);
            ASPxGridView1_MaterialDetail.DataSource = orderMaterialDetail;
            ASPxGridView1_MaterialDetail.DataBind();
        }
        private void LoadOrderMaterialPrice()
        {
            var orderMaterialPrice = _OrderMaterialPriceRepo.GetByOrderMaterialId(id);
            ASPxGridViewPrice.DataSource = orderMaterialPrice;
            ASPxGridViewPrice.DataBind();
        }
        #endregion

        #region Savedata
        private void Save(string strLink = "")
        {
            try
            {
                var orderMaterial = _OrderMaterialRepo.GetById(id);
                if (id > 0 && orderMaterial != null)
                {
                    orderMaterial.CODE = txtCode.Text;
                    orderMaterial.ORDERPRICE = Utils.CDecDef(txtOrderPrice.Text.Replace(",",""));
                    orderMaterial.COMPANY_ID = Utils.CIntDef(ddlCompany.SelectedValue);
                    orderMaterial.CREATED_DATE = pickerAndCalendarDate.returnDate;
                    orderMaterial.NOTE = txtNote.Text;

                    orderMaterial.CREATOR_ID = Utils.CIntDef(Session["Userid"]);

                    _OrderMaterialRepo.Update(orderMaterial);

                    strLink = string.IsNullOrEmpty(strLink) ? "nhap-nguyen-lieu.aspx?id=" + id : strLink;
                }
                else
                {
                    orderMaterial = new ORDERMATERIAL();
                    orderMaterial.CODE = txtCode.Text;
                    orderMaterial.ORDERPRICE = Utils.CDecDef(txtOrderPrice.Text.Replace(",", ""));
                    orderMaterial.COMPANY_ID = Utils.CIntDef(ddlCompany.SelectedValue);
                    orderMaterial.CREATED_DATE = pickerAndCalendarDate.returnDate;
                    orderMaterial.NOTE = txtNote.Text;
                    orderMaterial.TYPE = Cost.NHAP_NGUYENLIEU;

                    orderMaterial.CREATOR_ID = Utils.CIntDef(Session["Userid"]);

                    _OrderMaterialRepo.Create(orderMaterial);

                    strLink = string.IsNullOrEmpty(strLink) ? "nhap-nguyen-lieu.aspx?id=" + orderMaterial.ID : strLink;
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
            Save("danh-sach-nhap-nguyen-lieu.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("nhap-nguyen-lieu.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _OrderMaterialRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-nhap-nguyen-lieu.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-nhap-nguyen-lieu.aspx");
        }

        protected void lbtnSaveDetail_Click(object sender, EventArgs e)
        {
            try
            {

                var orderMaterialDetail = _OrderMaterialDetailRepo.GetByOrderMaterialIdAndMaterialId(id, Utils.CIntDef(ddlMaterial.SelectedValue));
                if (orderMaterialDetail != null)
                {
                    orderMaterialDetail.ORDERMATERIAL_ID = id;
                    orderMaterialDetail.MATERIAL_ID = Utils.CIntDef(ddlMaterial.SelectedValue);
                    orderMaterialDetail.QUANTITY = Utils.CIntDef(txtQuantity.Text.Replace(",", ""));
                    orderMaterialDetail.PRICE = Utils.CIntDef(txtPrice.Text.Replace(",", ""));
                    orderMaterialDetail.SUBTOTAL = Utils.CIntDef(txtPrice.Text.Replace(",", "")) * Utils.CIntDef(txtQuantity.Text.Replace(",", ""));

                    _OrderMaterialDetailRepo.Update(orderMaterialDetail);
                }
                else
                {
                    orderMaterialDetail = new ORDERMATERIAL_DETAIL();
                    orderMaterialDetail.ORDERMATERIAL_ID = id;
                    orderMaterialDetail.MATERIAL_ID = Utils.CIntDef(ddlMaterial.SelectedValue);
                    orderMaterialDetail.QUANTITY = Utils.CIntDef(txtQuantity.Text.Replace(",", ""));
                    orderMaterialDetail.PRICE = Utils.CIntDef(txtPrice.Text.Replace(",", ""));
                    orderMaterialDetail.SUBTOTAL = Utils.CIntDef(txtPrice.Text.Replace(",", "")) * Utils.CIntDef(txtQuantity.Text.Replace(",", ""));

                    _OrderMaterialDetailRepo.Create(orderMaterialDetail);
                }
                Response.Redirect("nhap-nguyen-lieu.aspx?id=" + id + "&activetab=" + 1);
            }
            catch
            {
            }
        }

        protected void lbtnDeleteDetail_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_MaterialDetail.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _OrderMaterialDetailRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("nhap-nguyen-lieu.aspx?id=" + id + "&activetab=" + 1);
        }
        protected void lbtnSaveDetailPrice_Click(object sender, EventArgs e)
        {
            try
            {
                var orderMaterialPrice = new ORDERMATERIAL_PRICE();
                orderMaterialPrice.ORDERMATERIAL_ID = id;
                orderMaterialPrice.PRICE = Utils.CIntDef(txtPriceTra.Text.Replace(",", ""));
                orderMaterialPrice.CREATE_DATE = pickerAndCalendarNgaytra.returnDate;

                _OrderMaterialPriceRepo.Create(orderMaterialPrice);

                Response.Redirect("nhap-nguyen-lieu.aspx?id=" + id + "&activetab=" + 2);
            }
            catch
            {
            }
        }
        protected void lbtnDeleteDetailPrice_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridViewPrice.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _OrderMaterialPriceRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("nhap-nguyen-lieu.aspx?id=" + id + "&activetab=" + 2);
        }
        #endregion
        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _id = Utils.CIntDef(ddlMaterial.SelectedValue);
            var Material = _MaterialRepo.GetById(_id);
            if (Material != null)
            {
                var Unit = _UnitRepo.GetById(Utils.CIntDef(Material.UNIT_ID));
                if (Unit != null)
                {
                    lbUnit.Text = Unit.NAME;
                }
            }
            ASPxPageControl2.ActiveTabIndex = 1;
        }
        #region function
        public string getMaterialName(object oid)
        {
            int _id = Utils.CIntDef(oid);
            var item = _MaterialRepo.GetById(_id);
            if (item != null)
            {
                return item.NAME;
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
        public string getSumQuantity()
        {
            var orderMaterialDetail = _OrderMaterialDetailRepo.GetByOrderMaterialId(id);
            if (orderMaterialDetail != null)
            {
                return cls.FormatMoneyNotext(orderMaterialDetail.Sum(n => n.QUANTITY));
            }
            return "";
        }
        public string getSumSubtotal()
        {
            var orderMaterialDetail = _OrderMaterialDetailRepo.GetByOrderMaterialId(id);
            if (orderMaterialDetail != null)
            {
                return cls.FormatMoneyNotext(orderMaterialDetail.Sum(n => n.SUBTOTAL));
            }
            return "";
        }
        public string getSumTientra()
        {
            var orderMaterialPrice = _OrderMaterialPriceRepo.GetByOrderMaterialId(id);
            if (orderMaterialPrice != null)
            {
                return cls.FormatMoneyNotext(orderMaterialPrice.Sum(n => n.PRICE));
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
        
        public string getFormatQuantity(object quantity)
        {
            return cls.FormatMoneyNotext(quantity);
        }
        
        #endregion
    }
}