using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class ProductDetailRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<PRODUCT_DETAIL> GetListByProductIdAndContainsAll(int productId, string strall)
        {
            return this.db.PRODUCT_DETAILs.Where(n => (n.PRODUCT_ID == productId || productId == 0) && (n.CODE.Contains(strall) || n.NAME.Contains(strall) || strall == "")).OrderBy(a => a.CODE).ToList();
        }
        public virtual List<PRODUCT_DETAIL> GetListByOrderId(int orderId)
        {
            var list = (from a in db.PRODUCT_DETAILs
                        join b in db.ORDER_DETAILs on a.ID equals b.PRODUCT_DETAIL_ID
                        where b.ORDER_ID == orderId
                        select a).OrderByDescending(n=>n.CODE).ToList();
            return list;         
        }
        public virtual PRODUCT_DETAIL GetById(int id)
        {
            try
            {
                return this.db.PRODUCT_DETAILs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<PRODUCT_DETAIL> GetAll()
        {
            return this.db.PRODUCT_DETAILs.OrderBy(n => n.CODE).ToList();
        }
        public virtual void Create(PRODUCT_DETAIL cus)
        {
            try
            {
                this.db.PRODUCT_DETAILs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(PRODUCT_DETAIL cus)
        {
            try
            {
                PRODUCT_DETAIL cusOld = this.GetById(cus.ID);
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
                PRODUCT_DETAIL cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(PRODUCT_DETAIL cus)
        {
            try
            {
                db.PRODUCT_DETAILs.DeleteOnSubmit(cus);
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
                PRODUCT_DETAIL cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(PRODUCT_DETAIL cus)
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