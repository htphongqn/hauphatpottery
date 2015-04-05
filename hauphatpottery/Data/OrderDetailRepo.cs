using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderDetailRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual ORDER_DETAIL GetByProductDetailId(int productDetailId, int productDetailSizeId, int orderId)
        {
            try
            {
                return this.db.ORDER_DETAILs.Single(u => u.PRODUCT_DETAIL_ID == productDetailId && u.PRODUCT_DETAIL_SIZE_ID == productDetailSizeId && u.ORDER_ID == orderId);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<ORDER_DETAIL> GetByOrderId(int orderId)
        {
            return this.db.ORDER_DETAILs.Where(n => n.ORDER_ID == orderId).OrderByDescending(n => n.PRODUCT_DETAIL.CODE).ToList();
        }
        public virtual ORDER_DETAIL GetById(int id)
        {
            try
            {
                return this.db.ORDER_DETAILs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDER_DETAIL> GetAll()
        {
            return this.db.ORDER_DETAILs.ToList();
        }
        public virtual void Create(ORDER_DETAIL user)
        {
            try
            {
                this.db.ORDER_DETAILs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDER_DETAIL user)
        {
            try
            {
                ORDER_DETAIL userOld = this.GetById(user.ID);
                userOld = user;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(int id)
        {
            try
            {
                ORDER_DETAIL user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDER_DETAIL user)
        {
            try
            {
                db.ORDER_DETAILs.DeleteOnSubmit(user);
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
                ORDER_DETAIL user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDER_DETAIL user)
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