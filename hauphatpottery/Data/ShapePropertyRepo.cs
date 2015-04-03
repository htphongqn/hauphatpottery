using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class ShapePropertyRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual SHAPE_PROPERTY GetByProductDetailId(int productDetailId)
        {
            try
            {
                var item = (from a in db.SHAPE_PROPERTies
                            join b in db.PRODUCTs on a.SHAPE_CODE equals b.SHAPE_CODE
                            join c in db.PRODUCT_DETAILs on b.ID equals c.PRODUCT_ID
                            where c.ID == productDetailId
                            select a).Single();
                return item;
            }
            catch
            {
                return null;
            }
        }
        public virtual SHAPE_PROPERTY GetByCode(string code)
        {
            try
            {
                return this.db.SHAPE_PROPERTies.Single(u => u.SHAPE_CODE == code);
            }
            catch
            {
                return null;
            }
        }
        public virtual SHAPE_PROPERTY GetById(int id)
        {
            try
            {
                return this.db.SHAPE_PROPERTies.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<SHAPE_PROPERTY> GetAll()
        {
            return this.db.SHAPE_PROPERTies.OrderBy(n => n.ID).ToList();
        }
        public virtual void Create(SHAPE_PROPERTY cus)
        {
            try
            {
                this.db.SHAPE_PROPERTies.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch// (Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(SHAPE_PROPERTY cus)
        {
            try
            {
                SHAPE_PROPERTY cusOld = this.GetById(cus.ID);
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
                SHAPE_PROPERTY cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(SHAPE_PROPERTY cus)
        {
            try
            {
                db.SHAPE_PROPERTies.DeleteOnSubmit(cus);
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
                SHAPE_PROPERTY cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(SHAPE_PROPERTY cus)
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