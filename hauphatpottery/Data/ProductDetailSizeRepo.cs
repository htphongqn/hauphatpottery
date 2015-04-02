using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class ProductDetailSizeRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<TYPE> GetListByContainsName(string name)
        {
            return this.db.TYPEs.Where(n => n.NAME.Contains(name)).OrderBy(a => a.NAME).ToList();
        }
        public virtual TYPE GetById(int id)
        {
            try
            {
                return this.db.TYPEs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<TYPE> GetAll()
        {
            return this.db.TYPEs.OrderBy(n => n.NAME).ToList();
        }
        public virtual void Create(TYPE cus)
        {
            try
            {
                this.db.TYPEs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(TYPE cus)
        {
            try
            {
                TYPE cusOld = this.GetById(cus.ID);
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
                TYPE cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(TYPE cus)
        {
            try
            {
                db.TYPEs.DeleteOnSubmit(cus);
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
                TYPE cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(TYPE cus)
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