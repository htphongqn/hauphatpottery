using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class OrderDeadlineOrderDetailRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<ORDER_DEADLINE_ORDER_DETAIL> GetListByOrderDeadlineId(int OrderDeadlineId)
        {
            return this.db.ORDER_DEADLINE_ORDER_DETAILs.Where(n => n.ORDER_DEADLINE_ID == OrderDeadlineId).ToList();
        }
        public virtual ORDER_DEADLINE_ORDER_DETAIL GetByOrderdeadlineIdAndOrderdetailId(int OrderDeadlineId, int Orderdetailid)
        {
            try
            {
                return this.db.ORDER_DEADLINE_ORDER_DETAILs.Single(u => u.ORDER_DEADLINE_ID == OrderDeadlineId && u.ORDER_DETAIL_ID == Orderdetailid);
            }
            catch
            {
                return null;
            }
        }
        public virtual ORDER_DEADLINE_ORDER_DETAIL GetById(int id)
        {
            try
            {
                return this.db.ORDER_DEADLINE_ORDER_DETAILs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<ORDER_DEADLINE_ORDER_DETAIL> GetAll()
        {
            return this.db.ORDER_DEADLINE_ORDER_DETAILs.ToList();
        }
        public virtual void Create(ORDER_DEADLINE_ORDER_DETAIL cus)
        {
            try
            {
                this.db.ORDER_DEADLINE_ORDER_DETAILs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDER_DEADLINE_ORDER_DETAIL cus)
        {
            try
            {
                ORDER_DEADLINE_ORDER_DETAIL cusOld = this.GetById(cus.ID);
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
                ORDER_DEADLINE_ORDER_DETAIL cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDER_DEADLINE_ORDER_DETAIL cus)
        {
            try
            {
                db.ORDER_DEADLINE_ORDER_DETAILs.DeleteOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(List<ORDER_DEADLINE_ORDER_DETAIL> cus)
        {
            try
            {
                db.ORDER_DEADLINE_ORDER_DETAILs.DeleteAllOnSubmit(cus);
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
                ORDER_DEADLINE_ORDER_DETAIL cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDER_DEADLINE_ORDER_DETAIL cus)
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