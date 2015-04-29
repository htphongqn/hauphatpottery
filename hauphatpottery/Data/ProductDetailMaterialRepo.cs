using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class ProductDetailMaterialRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<PRODUCT_DETAIL_MATERIAL> GetByProductDetailId(int productDetailId, int productDetailSizeId)
        {
            return this.db.PRODUCT_DETAIL_MATERIALs.Where(n => n.PRODUCT_DETAIL_ID == productDetailId && n.PRODUCT_DETAIL_SIZE_ID == productDetailSizeId).ToList();
        }
        public virtual List<PRODUCT_DETAIL_MATERIAL> GetByProductDetailId(int productDetailId)
        {
            return this.db.PRODUCT_DETAIL_MATERIALs.Where(n => n.PRODUCT_DETAIL_ID == productDetailId).ToList();
        }
        public virtual PRODUCT_DETAIL_MATERIAL GetByProductDetailIdAndMaterialId(int productDetailId, int productDetailSizeId, int materialid)
        {
            try
            {
                return this.db.PRODUCT_DETAIL_MATERIALs.Single(u => u.PRODUCT_DETAIL_ID == productDetailId && u.PRODUCT_DETAIL_SIZE_ID == productDetailSizeId && u.MATERIAL_ID == materialid);
            }
            catch
            {
                return null;
            }
        }
        public virtual PRODUCT_DETAIL_MATERIAL GetById(int id)
        {
            try
            {
                return this.db.PRODUCT_DETAIL_MATERIALs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<PRODUCT_DETAIL_MATERIAL> GetAll()
        {
            return this.db.PRODUCT_DETAIL_MATERIALs.ToList();
        }
        public virtual void Create(PRODUCT_DETAIL_MATERIAL cus)
        {
            try
            {
                this.db.PRODUCT_DETAIL_MATERIALs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(PRODUCT_DETAIL_MATERIAL cus)
        {
            try
            {
                PRODUCT_DETAIL_MATERIAL cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(int id)
        {
            try
            {
                PRODUCT_DETAIL_MATERIAL cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(PRODUCT_DETAIL_MATERIAL cus)
        {
            try
            {
                db.PRODUCT_DETAIL_MATERIALs.DeleteOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(int id)
        {
            try
            {
                PRODUCT_DETAIL_MATERIAL cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(PRODUCT_DETAIL_MATERIAL cus)
        {
            try
            {
                //user.IsDelete = true;
                db.SubmitChanges();
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}