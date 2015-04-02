using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class CustomerRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<CUSTOMER> GetListByContainsFullName(string name)
        {
            return this.db.CUSTOMERs.Where(n => n.FULLNAME.Contains(name)).OrderBy(a => a.FULLNAME).ToList();
        }
        public virtual List<CUSTOMER> GetListByContainsPhone(string phone)
        {
            return this.db.CUSTOMERs.Where(n => n.PHONE.Contains(phone)).OrderBy(a => a.PHONE).ToList();
        }
        public virtual List<CUSTOMER> GetListByContainsAddress(string address)
        {
            return this.db.CUSTOMERs.Where(n => n.ADDRESS.Contains(address)).OrderBy(a => a.ADDRESS).ToList();
        }
        public virtual List<CUSTOMER> GetListByContainsEmail(string email)
        {
            return this.db.CUSTOMERs.Where(n => n.EMAIL.Contains(email)).OrderBy(a => a.EMAIL).ToList();
        }
        public virtual List<CUSTOMER> GetListByContainsAll(string strAll)
        {
            return this.db.CUSTOMERs.Where(n => (n.FULLNAME.Contains(strAll) || n.PHONE.Contains(strAll) || n.ADDRESS.Contains(strAll) || n.EMAIL.Contains(strAll) || strAll == "")).OrderBy(n => n.FULLNAME).ToList();
        }
        public virtual CUSTOMER GetById(int id)
        {
            try
            {
                return this.db.CUSTOMERs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CUSTOMER> GetAll()
        {
            return this.db.CUSTOMERs.OrderBy(n => n.FULLNAME).ToList();
        }
        public virtual void Create(CUSTOMER cus)
        {
            try
            {
                this.db.CUSTOMERs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(CUSTOMER cus)
        {
            try
            {
                CUSTOMER cusOld = this.GetById(cus.ID);
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
                CUSTOMER cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CUSTOMER cus)
        {
            try
            {
                db.CUSTOMERs.DeleteOnSubmit(cus);
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
                CUSTOMER cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CUSTOMER cus)
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