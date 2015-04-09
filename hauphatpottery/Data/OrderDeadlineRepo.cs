using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderDeadlineRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<ORDER_DEADLINE> GetByOrderId(int OrderId)
        {
            return this.db.ORDER_DEADLINEs.Where(n => n.ORDER_ID == OrderId).ToList();
        }
        public virtual ORDER_DEADLINE GetById(int id)
        {
            try
            {
                return this.db.ORDER_DEADLINEs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDER_DEADLINE> GetAll()
        {
            return this.db.ORDER_DEADLINEs.ToList();
        }
        public virtual void Create(ORDER_DEADLINE user)
        {
            try
            {
                this.db.ORDER_DEADLINEs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDER_DEADLINE user)
        {
            try
            {
                ORDER_DEADLINE userOld = this.GetById(user.ID);
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
                ORDER_DEADLINE user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDER_DEADLINE user)
        {
            try
            {
                db.ORDER_DEADLINEs.DeleteOnSubmit(user);
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
                ORDER_DEADLINE user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDER_DEADLINE user)
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