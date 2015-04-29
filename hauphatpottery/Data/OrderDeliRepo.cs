using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderDeliRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<ORDER_DELI> GetByWhereAll(string s, int orderId, int userId, DateTime datefrom, DateTime dateto)
        {
            return this.db.ORDER_DELIs.Where(n => (n.CODE.Contains(s) || s == "")
                && (n.ORDER_ID == orderId || orderId == 0)
                && (n.CREATOR_ID == userId || userId == 0)
                && (n.CREATOR_ID == userId || userId == 0)
                && (n.CREATE_DATE.Value.Date >= datefrom.Date && n.CREATE_DATE.Value.Date <= dateto.Date)
                ).OrderByDescending(n => n.ID).ToList();
        }
        public virtual List<ORDER_DELI> GetByOrderId(int orderId, string s)
        {
            return this.db.ORDER_DELIs.Where(n => (n.ORDER_ID == orderId || orderId == 0) || (n.CODE.Contains(s) || s == "")).ToList();
        }
        public virtual ORDER_DELI GetById(int id)
        {
            try
            {
                return this.db.ORDER_DELIs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDER_DELI> GetAll()
        {
            return this.db.ORDER_DELIs.ToList();
        }
        public virtual void Create(ORDER_DELI user)
        {
            try
            {
                this.db.ORDER_DELIs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDER_DELI user)
        {
            try
            {
                ORDER_DELI userOld = this.GetById(user.ID);
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
                ORDER_DELI user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDER_DELI user)
        {
            try
            {
                db.ORDER_DELIs.DeleteOnSubmit(user);
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
                ORDER_DELI user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDER_DELI user)
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