using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderMaterialRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<ORDERMATERIAL> GetByType(string str, int companyId, int orderId, int typeId)
        {
            return this.db.ORDERMATERIALs.Where(n => n.TYPE == typeId && n.CODE.Contains(str) 
                && (n.COMPANY_ID == companyId || companyId == 0)
                && (n.ORDER_ID == orderId || orderId == 0)).ToList();
        }
        public virtual ORDERMATERIAL GetById(int id)
        {
            try
            {
                return this.db.ORDERMATERIALs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDERMATERIAL> GetAll()
        {
            return this.db.ORDERMATERIALs.ToList();
        }
        public virtual void Create(ORDERMATERIAL user)
        {
            try
            {
                this.db.ORDERMATERIALs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDERMATERIAL user)
        {
            try
            {
                ORDERMATERIAL userOld = this.GetById(user.ID);
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
                ORDERMATERIAL user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDERMATERIAL user)
        {
            try
            {
                db.ORDERMATERIALs.DeleteOnSubmit(user);
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
                ORDERMATERIAL user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDERMATERIAL user)
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