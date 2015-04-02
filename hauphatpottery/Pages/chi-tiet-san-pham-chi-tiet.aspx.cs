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
    public partial class chi_tiet_san_pham_chi_tiet : System.Web.UI.Page
    {
        #region Declare
        private ProductRepo _ProductRepo = new ProductRepo();
        private ProductDetailRepo _ProductDetailRepo = new ProductDetailRepo();
        private TypeRepo _TypeRepo = new TypeRepo();
        private ShapeRepo _ShapeRepo = new ShapeRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("chi-tiet-san-pham-chi-tiet.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            if (!IsPostBack)
            {
                LoadProduct();
                LoadType();
                LoadShape();
                getInfo();
            }
        }
        private void LoadProduct()
        {
            var list = _ProductRepo.GetAll();
            ddlProduct.DataSource = list;
            ddlProduct.DataBind();
        }
        private void LoadType()
        {
            var list = _TypeRepo.GetAll();
            ddlType.DataSource = list;
            ddlType.DataBind();
        }
        private void LoadShape()
        {
            var list = _ShapeRepo.GetAll();
            ddlShape.DataSource = list;
            ddlShape.DataBind();
        }

        #region getInfo
        private void getInfo()
        {
            try
            {
                var productDetails = _ProductDetailRepo.GetById(id);
                if (productDetails != null)
                {
                    txtCode.Text = productDetails.CODE;
                    txtName.Text = productDetails.NAME;
                    txtM2.Text = Utils.CStrDef(productDetails.M2);
                    txtWeight.Text = Utils.CDecDef(productDetails.WEIGHT) > 0 ? Utils.CStrDef(productDetails.WEIGHT) : "";
                    ddlType.SelectedValue = Utils.CStrDef(productDetails.TYPE_ID);
                    ddlProduct.SelectedValue = Utils.CStrDef(productDetails.PRODUCT_ID);
                    ddlShape.SelectedValue = Utils.CStrDef(productDetails.SHAPE_CODE);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Savedata
        private void Save(string strLink = "")
        {
            try
            {
                var ProductDetail = _ProductDetailRepo.GetById(id);
                if (id > 0 && ProductDetail != null)
                {
                    ProductDetail.CODE = txtCode.Text;
                    ProductDetail.NAME = txtName.Text;
                    ProductDetail.SHAPE_CODE = ddlShape.SelectedValue;
                    ProductDetail.M2 = Utils.CDecDef(txtM2.Text);
                    ProductDetail.WEIGHT = Utils.CDecDef(txtWeight.Text); ;
                    ProductDetail.TYPE_ID = Utils.CIntDef(ddlType.SelectedValue);
                    ProductDetail.PRODUCT_ID = Utils.CIntDef(ddlProduct.SelectedValue);

                    _ProductDetailRepo.Update(ProductDetail);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-san-pham-chi-tiet.aspx?id=" + id : strLink;
                }
                else
                {
                    ProductDetail = new PRODUCT_DETAIL();
                    ProductDetail.CODE = txtCode.Text;
                    ProductDetail.NAME = txtName.Text;
                    ProductDetail.SHAPE_CODE = ddlShape.SelectedValue;
                    ProductDetail.M2 = Utils.CDecDef(txtM2.Text);
                    ProductDetail.WEIGHT = Utils.CDecDef(txtWeight.Text); ;
                    ProductDetail.TYPE_ID = Utils.CIntDef(ddlType.SelectedValue);
                    ProductDetail.PRODUCT_ID = Utils.CIntDef(ddlProduct.SelectedValue);

                    _ProductDetailRepo.Create(ProductDetail);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-san-pham-chi-tiet.aspx?id=" + ProductDetail.ID : strLink;
                }
            }
            catch (Exception)
            {
                throw;
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
            Save("danh-sach-san-pham-chi-tiet.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-san-pham-chi-tiet.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductDetailRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-san-pham-chi-tiet.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-san-pham-chi-tiet.aspx");
        }
        #endregion

    }
}