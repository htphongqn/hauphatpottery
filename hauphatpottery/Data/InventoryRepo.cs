using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class InventoryRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<INVENTORY> GetByOrderIdAndProductDetailId(int orderId, int productDetailId, int type)
        {
            return this.db.INVENTORies.Where(n => n.ORDER_ID == orderId && n.PRODUCT_DETAIL_ID == productDetailId && n.TYPE == type).OrderByDescending(n=>n.CREATE_DATE).ToList();
        }
        public virtual INVENTORY GetById(int id)
        {
            try
            {
                return this.db.INVENTORies.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<INVENTORY> GetAll()
        {
            return this.db.INVENTORies.OrderBy(n => n.ID).ToList();
        }
        public virtual void Create(INVENTORY cus)
        {
            try
            {
                this.db.INVENTORies.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(INVENTORY cus)
        {
            try
            {
                INVENTORY cusOld = this.GetById(cus.ID);
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
                INVENTORY cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(INVENTORY cus)
        {
            try
            {
                db.INVENTORies.DeleteOnSubmit(cus);
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
                INVENTORY cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(INVENTORY cus)
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