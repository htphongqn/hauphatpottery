using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class ShapeRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<SHAPE> GetListByContainsName(string name)
        {
            return this.db.SHAPEs.Where(n => n.NAME.Contains(name)).OrderBy(a => a.NAME).ToList();
        }
        public virtual SHAPE GetById(int id)
        {
            try
            {
                return this.db.SHAPEs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<SHAPE> GetAll()
        {
            return this.db.SHAPEs.OrderBy(n => n.NAME).ToList();
        }
        public virtual void Create(SHAPE cus)
        {
            try
            {
                this.db.SHAPEs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(SHAPE cus)
        {
            try
            {
                SHAPE cusOld = this.GetById(cus.ID);
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
                SHAPE cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(SHAPE cus)
        {
            try
            {
                db.SHAPEs.DeleteOnSubmit(cus);
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
                SHAPE cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(SHAPE cus)
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