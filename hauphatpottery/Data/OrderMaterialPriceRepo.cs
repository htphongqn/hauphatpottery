using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderMaterialPriceRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<ORDERMATERIAL_PRICE> GetByOrderMaterialId(int ORDERMATERIAL_PRICE)
        {
            return this.db.ORDERMATERIAL_PRICEs.Where(n =>n.ORDERMATERIAL_ID == ORDERMATERIAL_PRICE).ToList();
        }
        public virtual ORDERMATERIAL_PRICE GetById(int id)
        {
            try
            {
                return this.db.ORDERMATERIAL_PRICEs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDERMATERIAL_PRICE> GetAll()
        {
            return this.db.ORDERMATERIAL_PRICEs.ToList();
        }
        public virtual void Create(ORDERMATERIAL_PRICE user)
        {
            try
            {
                this.db.ORDERMATERIAL_PRICEs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDERMATERIAL_PRICE user)
        {
            try
            {
                ORDERMATERIAL_PRICE userOld = this.GetById(user.ID);
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
                ORDERMATERIAL_PRICE user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDERMATERIAL_PRICE user)
        {
            try
            {
                db.ORDERMATERIAL_PRICEs.DeleteOnSubmit(user);
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
                ORDERMATERIAL_PRICE user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDERMATERIAL_PRICE user)
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