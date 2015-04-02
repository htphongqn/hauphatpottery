using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class ProductRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<PRODUCT> GetListByContainsCode(string code)
        {
            return this.db.PRODUCTs.Where(n => n.CODE.Contains(code)).OrderBy(a => a.CODE).ToList();
        }
        public virtual PRODUCT GetById(int id)
        {
            try
            {
                return this.db.PRODUCTs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<PRODUCT> GetAll()
        {
            return this.db.PRODUCTs.OrderBy(n => n.CODE).ToList();
        }
        public virtual void Create(PRODUCT cus)
        {
            try
            {
                this.db.PRODUCTs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(PRODUCT cus)
        {
            try
            {
                PRODUCT cusOld = this.GetById(cus.ID);
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
                PRODUCT cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(PRODUCT cus)
        {
            try
            {
                db.PRODUCTs.DeleteOnSubmit(cus);
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
                PRODUCT cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(PRODUCT cus)
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