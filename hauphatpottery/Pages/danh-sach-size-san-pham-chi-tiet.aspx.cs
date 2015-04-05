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
    public partial class danh_sach_size_san_pham_chi_tiet : System.Web.UI.Page
    {
        #region Declare
        private ShapePropertyRepo _ShapePropertyRepo = new ShapePropertyRepo();
        private UnitRepo _UnitRepo = new UnitRepo();
        private ProductDetailRepo _ProductDetailRepo = new ProductDetailRepo();
        private ProductDetailSizeRepo _ProductDetailSizeRepo = new ProductDetailSizeRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("danh-sach-size-san-pham-chi-tiet.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadProductDetail();
                SetShapeProperty();
                LoadProductDetail_Size();
            }
            else
            {
                ASPxGridView1_ProductDetail_Size.DataSource = HttpContext.Current.Session["listProductSize"];
                ASPxGridView1_ProductDetail_Size.DataBind();
            }
        }
        private void SetShapeProperty()
        {
            int productDetailId = Utils.CIntDef(ddLProductDetail.SelectedValue);
            var item = _ShapePropertyRepo.GetByProductDetailId(productDetailId);
            if (item != null)
            {
                tdD.Visible = Utils.CIntDef(item.D) == 1 ? true : false;
                tdH.Visible = Utils.CIntDef(item.H) == 1 ? true : false;
                tdL.Visible = Utils.CIntDef(item.L) == 1 ? true : false;
                tdW.Visible = Utils.CIntDef(item.W) == 1 ? true : false;

                ASPxGridView1_ProductDetail_Size.Columns["D"].Visible = Utils.CIntDef(item.D) == 1 ? true : false;
                ASPxGridView1_ProductDetail_Size.Columns["H"].Visible = Utils.CIntDef(item.H) == 1 ? true : false;
                ASPxGridView1_ProductDetail_Size.Columns["L"].Visible = Utils.CIntDef(item.L) == 1 ? true : false;
                ASPxGridView1_ProductDetail_Size.Columns["W"].Visible = Utils.CIntDef(item.W) == 1 ? true : false;
            }
        }
        private void LoadProductDetail()
        {
            try
            {
                var list = _ProductDetailRepo.GetAll();
                ddLProductDetail.DataSource = list;
                ddLProductDetail.DataBind();
                ddLProductDetail.SelectedValue = Utils.CStrDef(id);
            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void ddLProductDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetShapeProperty();
            LoadProductDetail_Size();
        }
        private void LoadProductDetail_Size()
        {
            try
            {
                int productDetailId = Utils.CIntDef(ddLProductDetail.SelectedValue);
                var list = _ProductDetailSizeRepo.GetByProductDetailId(productDetailId);

                HttpContext.Current.Session["listProductSize"] = list;
                ASPxGridView1_ProductDetail_Size.DataSource = list;
                ASPxGridView1_ProductDetail_Size.DataBind();
            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_ProductDetail_Size.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _ProductDetailSizeRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("danh-sach-size-san-pham-chi-tiet.aspx?id=" + id);
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
        public string getCodeProductDetail(object productDetailId)
        {
            int _id = Utils.CIntDef(productDetailId);
            var ProductDetail = _ProductDetailRepo.GetById(_id);
            if (ProductDetail != null)
            {
                return ProductDetail.CODE;
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
        public string getNameProductDetail(object productDetailId)
        {
            int _id = Utils.CIntDef(productDetailId);
            var ProductDetail = _ProductDetailRepo.GetById(_id);
            if (ProductDetail != null)
            {
                return ProductDetail.NAME;
            }
            return "";
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
                int productDetailId = Utils.CIntDef(ddLProductDetail.SelectedValue);

                var ProductDetailSize = new PRODUCT_DETAIL_SIZE();
                ProductDetailSize.PRODUCT_DETAIL_ID = Utils.CIntDef(ddLProductDetail.SelectedValue);
                ProductDetailSize.D = Utils.CIntDef(txtD.Value);
                ProductDetailSize.H = Utils.CIntDef(txtH.Value);
                ProductDetailSize.L = Utils.CIntDef(txtL.Value);
                ProductDetailSize.W = Utils.CIntDef(txtW.Value);
                _ProductDetailSizeRepo.Create(ProductDetailSize);
            }
            catch
            {

            }
            finally
            {
                Response.Redirect("danh-sach-size-san-pham-chi-tiet.aspx?id=" + ddLProductDetail.SelectedValue);
            }
        }
    }
}