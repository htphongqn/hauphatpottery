﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class ProductDetailSizeRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual List<PRODUCT_DETAIL_SIZE> GetByProductDetailId(int productDetailId)
        {
            return this.db.PRODUCT_DETAIL_SIZEs.Where(n=>n.PRODUCT_DETAIL_ID == productDetailId).OrderBy(n => n.ID).ToList();
        }
        public virtual PRODUCT_DETAIL_SIZE GetById(int id)
        {
            try
            {
                return this.db.PRODUCT_DETAIL_SIZEs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<PRODUCT_DETAIL_SIZE> GetAll()
        {
            return this.db.PRODUCT_DETAIL_SIZEs.OrderBy(n => n.ID).ToList();
        }
        public virtual void Create(PRODUCT_DETAIL_SIZE cus)
        {
            try
            {
                this.db.PRODUCT_DETAIL_SIZEs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(PRODUCT_DETAIL_SIZE cus)
        {
            try
            {
                PRODUCT_DETAIL_SIZE cusOld = this.GetById(cus.ID);
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
                PRODUCT_DETAIL_SIZE cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(PRODUCT_DETAIL_SIZE cus)
        {
            try
            {
                db.PRODUCT_DETAIL_SIZEs.DeleteOnSubmit(cus);
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
                PRODUCT_DETAIL_SIZE cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(PRODUCT_DETAIL_SIZE cus)
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