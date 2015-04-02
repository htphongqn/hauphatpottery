using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class UnitRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<UNIT> GetListByContainsName(string name)
        {
            return this.db.UNITs.Where(n => n.NAME.Contains(name)).OrderBy(a => a.NAME).ToList();
        }
        public virtual UNIT GetById(int id)
        {
            try
            {
                return this.db.UNITs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<UNIT> GetAll()
        {
            return this.db.UNITs.OrderBy(n => n.NAME).ToList();
        }
        public virtual void Create(UNIT cus)
        {
            try
            {
                this.db.UNITs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(UNIT cus)
        {
            try
            {
                UNIT cusOld = this.GetById(cus.ID);
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
                UNIT cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(UNIT cus)
        {
            try
            {
                db.UNITs.DeleteOnSubmit(cus);
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
                UNIT cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(UNIT cus)
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