using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class CompanyRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<COMPANY> GetListByContainsName(string name)
        {
            return this.db.COMPANies.Where(n => n.NAME.Contains(name)).OrderBy(a => a.NAME).ToList();
        }
        public virtual List<COMPANY> GetListByContainsPhone(string phone)
        {
            return this.db.COMPANies.Where(n => n.PHONE.Contains(phone)).OrderBy(a => a.PHONE).ToList();
        }
        public virtual List<COMPANY> GetListByContainsAddress(string address)
        {
            return this.db.COMPANies.Where(n => n.ADDRESS.Contains(address)).OrderBy(a => a.ADDRESS).ToList();
        }
        public virtual List<COMPANY> GetListByContainsEmail(string email)
        {
            return this.db.COMPANies.Where(n => n.EMAIL.Contains(email)).OrderBy(a => a.EMAIL).ToList();
        }
        public virtual List<COMPANY> GetListByContainsAll(string strAll)
        {
            return this.db.COMPANies.Where(n => (n.NAME.Contains(strAll) || n.PHONE.Contains(strAll) || n.ADDRESS.Contains(strAll) || n.EMAIL.Contains(strAll) || strAll == "")).OrderBy(n => n.NAME).ToList();
        }
        public virtual COMPANY GetById(int id)
        {
            try
            {
                return this.db.COMPANies.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<COMPANY> GetAll()
        {
            return this.db.COMPANies.OrderBy(n => n.NAME).ToList();
        }
        public virtual void Create(COMPANY cus)
        {
            try
            {
                this.db.COMPANies.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(COMPANY cus)
        {
            try
            {
                COMPANY cusOld = this.GetById(cus.ID);
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
                COMPANY cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(COMPANY cus)
        {
            try
            {
                db.COMPANies.DeleteOnSubmit(cus);
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
                COMPANY cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(COMPANY cus)
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