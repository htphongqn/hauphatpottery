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
    public partial class chi_tiet_san_pham : System.Web.UI.Page
    {
        #region Declare
        private ProductRepo _ProductRepo = new ProductRepo();
        private int id = 0;
        private UnitDataRepo _UnitDataRepo = new UnitDataRepo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPermission = _UnitDataRepo.checkPermissionPage("chi-tiet-san-pham.aspx", Utils.CIntDef(Session["groupId"]), Utils.CIntDef(Session["groupType"]));
            if (!isPermission)
            {
                Response.Write("<script>alert('Bạn không có quyền truy cập vào trang này');location.href='trang-chu.aspx';</script>");
            }
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            if (!IsPostBack)
            {                
                getInfo();
            }
        }
        #region getInfo
        private void getInfo()
        {
            try
            {
                var products = _ProductRepo.GetById(id);
                if (products != null)
                {
                    txtCode.Text = products.CODE;
                    string image = products.IMAGE;
                    if (!string.IsNullOrEmpty(image))
                    {
                        imgProduct.Visible = true;
                        lnkEditImage.Visible = true;
                        FileUploadImage.Visible = false;
                        imgProduct.ImageUrl = Cost.PRODUCTPATH + image;
                    }
                    else
                    {
                        imgProduct.Visible = false;
                        lnkEditImage.Visible = false;
                        FileUploadImage.Visible = true;
                    }
                }
                else
                {
                    imgProduct.Visible = false;
                    lnkEditImage.Visible = false;
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
                var Product = _ProductRepo.GetById(id);
                if (id > 0 && Product != null)
                {
                    Product.CODE = txtCode.Text;
                    if (FileUploadImage.Visible && FileUploadImage.HasFile)
                    {
                        string imgName = Product.IMAGE;
                        string productPath = Server.MapPath(Cost.PRODUCTPATH) + imgName;
                        if (File.Exists(productPath))
                        {
                            File.Delete(productPath);
                        }

                        imgName = Product.ID + Path.GetExtension(FileUploadImage.FileName);
                        productPath = Server.MapPath(Cost.PRODUCTPATH) + imgName;
                        FileUploadImage.SaveAs(productPath);

                        Product.IMAGE = imgName;

                    }
                    _ProductRepo.Update(Product);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-san-pham.aspx?id=" + id : strLink;
                }
                else
                {
                    Product = new PRODUCT();
                    Product.CODE = txtCode.Text;
                    //Product.CREATOR_ID = Utils.CIntDef(Session["Userid"]);
                    //Product.CREATED_DATE = DateTime.Now;
                    _ProductRepo.Create(Product);

                    Product = _ProductRepo.GetById(Product.ID);
                    if (Product.ID > 0 && Product != null)
                    {
                        if (FileUploadImage.Visible && FileUploadImage.HasFile)
                        {
                            string imgName = Product.ID + Path.GetExtension(FileUploadImage.FileName);
                            FileUploadImage.SaveAs(Server.MapPath(Cost.PRODUCTPATH) + imgName);

                            Product.IMAGE = imgName;

                            _ProductRepo.Update(Product);
                        }
                    }
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-san-pham.aspx?id=" + Product.ID : strLink;
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
            Save("danh-sach-san-pham.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-san-pham.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-san-pham.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-san-pham.aspx");
        }
        #endregion

        protected void lnkEditImage_Click(object sender, EventArgs e)
        {
            imgProduct.Visible = false;
            lnkEditImage.Visible = false;
            FileUploadImage.Visible = true;
        }

    }
}