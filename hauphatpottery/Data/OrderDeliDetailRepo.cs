using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderDeliDetailRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<ORDER_DELI_DETAIL> GetByProductDetailId(int productDetailId, int orderDeliId)
        {
            try
            {
                return this.db.ORDER_DELI_DETAILs.Where(u => u.PRODUCT_DETAIL_ID == productDetailId && u.ORDER_DELI_ID == orderDeliId).ToList();
            }
            catch
            {
                return null;
            }
        }
        public virtual ORDER_DELI_DETAIL GetByProductDetailId(int productDetailId, int productDetailSizeId, int orderDeliId)
        {
            try
            {
                return this.db.ORDER_DELI_DETAILs.Single(u => u.PRODUCT_DETAIL_ID == productDetailId && u.PRODUCT_DETAIL_SIZE_ID == productDetailSizeId && u.ORDER_DELI_ID == orderDeliId);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<ORDER_DELI_DETAIL> GetByOrderDeliId(int orderDeliId)
        {
            return this.db.ORDER_DELI_DETAILs.Where(n => n.ORDER_DELI_ID == orderDeliId).ToList();
        }
        public virtual ORDER_DELI_DETAIL GetById(int id)
        {
            try
            {
                return this.db.ORDER_DELI_DETAILs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDER_DELI_DETAIL> GetAll()
        {
            return this.db.ORDER_DELI_DETAILs.ToList();
        }
        public virtual void Create(ORDER_DELI_DETAIL user)
        {
            try
            {
                this.db.ORDER_DELI_DETAILs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDER_DELI_DETAIL user)
        {
            try
            {
                ORDER_DELI_DETAIL userOld = this.GetById(user.ID);
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
                ORDER_DELI_DETAIL user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDER_DELI_DETAIL user)
        {
            try
            {
                db.ORDER_DELI_DETAILs.DeleteOnSubmit(user);
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
                ORDER_DELI_DETAIL user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDER_DELI_DETAIL user)
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