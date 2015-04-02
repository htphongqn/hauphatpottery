using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<ORDER> GetByWhereAll(string s, int customerId, int statusId, int userId)
        {
            return this.db.ORDERs.Where(n => (n.CODE.Contains(s) || s == "")
                && (n.CUSTOMER_ID == customerId || customerId == 0)
                && (n.CREATOR_ID == userId || userId == 0)
                && (n.STATUS == statusId || statusId == 0)).OrderByDescending(n => n.ID).ToList();
        }
        public virtual List<ORDER> GetByStatus(int statusId)
        {
            return this.db.ORDERs.Where(n => n.STATUS == statusId).ToList();
        }
        public virtual ORDER GetById(int id)
        {
            try
            {
                return this.db.ORDERs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDER> GetAll()
        {
            return this.db.ORDERs.ToList();
        }
        public virtual void Create(ORDER user)
        {
            try
            {
                this.db.ORDERs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDER user)
        {
            try
            {
                ORDER userOld = this.GetById(user.ID);
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
                ORDER user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDER user)
        {
            try
            {
                db.ORDERs.DeleteOnSubmit(user);
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
                ORDER user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDER user)
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