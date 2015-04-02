using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class MaterialRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<MATERIAL> GetListByContainsName(string name)
        {
            return this.db.MATERIALs.Where(n => n.NAME.Contains(name)).OrderBy(a => a.NAME).ToList();
        }
        public virtual MATERIAL GetById(int id)
        {
            try
            {
                return this.db.MATERIALs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<MATERIAL> GetAll()
        {
            return this.db.MATERIALs.OrderBy(n => n.NAME).ToList();
        }
        public virtual void Create(MATERIAL cus)
        {
            try
            {
                this.db.MATERIALs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(MATERIAL cus)
        {
            try
            {
                MATERIAL cusOld = this.GetById(cus.ID);
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
                MATERIAL cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(MATERIAL cus)
        {
            try
            {
                db.MATERIALs.DeleteOnSubmit(cus);
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
                MATERIAL cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(MATERIAL cus)
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