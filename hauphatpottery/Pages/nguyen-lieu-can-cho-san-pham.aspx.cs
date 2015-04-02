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
    public partial class nguyen_lieu_can_cho_san_pham : System.Web.UI.Page
    {
        #region Declare
        private MaterialRepo _MaterialRepo = new MaterialRepo();
        private UnitRepo _UnitRepo = new UnitRepo();
        private ProductDetailRepo _ProductDetailRepo = new ProductDetailRepo();
        private ProductDetailMaterialRepo _ProductDetailMaterialRepo = new ProductDetailMaterialRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("nguyen-lieu-can-cho-san-pham.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                LoadProductDetail();
                LoadMaterial();
                LoadProduct_Material();
            }
            else
            {
                ASPxGridView_ProductDetail_Material.DataSource = HttpContext.Current.Session["listProductMaterial"];
                ASPxGridView_ProductDetail_Material.DataBind();
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
        private void LoadMaterial()
        {
            try
            {
                var list = _MaterialRepo.GetAll();
                ddlMaterial.DataSource = list;
                ddlMaterial.DataBind();

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _id = Utils.CIntDef(ddlMaterial.SelectedValue);
            var Material = _MaterialRepo.GetById(_id);
            if (Material != null)
            {
                var Unit = _UnitRepo.GetById(_id);
                if (Unit != null)
                {
                    lbUnit.Text = Unit.NAME;
                }
            }
        }

        protected void ddLProductDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProduct_Material();
        }
        private void LoadProduct_Material()
        {
            try
            {
                int productDetailId = Utils.CIntDef(ddLProductDetail.SelectedValue);
                var list = _ProductDetailMaterialRepo.GetByProductDetailId(productDetailId);

                HttpContext.Current.Session["listProductMaterial"] = list;
                ASPxGridView_ProductDetail_Material.DataSource = list;
                ASPxGridView_ProductDetail_Material.DataBind();
            }
            catch //(Exception)
            {
                //throw;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView_ProductDetail_Material.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _ProductDetailMaterialRepo.Remove(Utils.CIntDef(item));
            }
            Response.Redirect("nguyen-lieu-can-cho-san-pham.aspx?id=" + id);
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
                return Material.UNIT.NAME;
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
                int materialId = Utils.CIntDef(ddlMaterial.SelectedValue);
                var ProductDetailMateria = _ProductDetailMaterialRepo.GetByProductDetailIdAndMaterialId(productDetailId, materialId);
                if (ProductDetailMateria != null)
                {
                    ProductDetailMateria.PRODUCT_DETAIL_ID = Utils.CIntDef(ddLProductDetail.SelectedValue);
                    ProductDetailMateria.MATERIAL_ID = Utils.CIntDef(ddlMaterial.SelectedValue);
                    ProductDetailMateria.QUANTITY = Utils.CDecDef(txtQuantity.Value);

                    _ProductDetailMaterialRepo.Update(ProductDetailMateria);
                }
                else
                {
                    ProductDetailMateria = new PRODUCT_DETAIL_MATERIAL();
                    ProductDetailMateria.PRODUCT_DETAIL_ID = Utils.CIntDef(ddLProductDetail.SelectedValue);
                    ProductDetailMateria.MATERIAL_ID = Utils.CIntDef(ddlMaterial.SelectedValue);
                    ProductDetailMateria.QUANTITY = Utils.CDecDef(txtQuantity.Value);

                    _ProductDetailMaterialRepo.Create(ProductDetailMateria);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Response.Redirect("nguyen-lieu-can-cho-san-pham.aspx?id=" + ddLProductDetail.SelectedValue);
            }
        }
    }
}