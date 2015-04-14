using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hauphatpottery.Data
{
    public class OrderMaterialDetailRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public virtual ORDERMATERIAL_DETAIL GetByOrderMaterialIdAndMaterialId(int orderMaterialId, int materialId)
        {
            try
            {
                return this.db.ORDERMATERIAL_DETAILs.Single(u => u.ORDERMATERIAL_ID == orderMaterialId && u.MATERIAL_ID == materialId);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<ORDERMATERIAL_DETAIL> GetByOrderMaterialId(int orderMaterialId)
        {
            return this.db.ORDERMATERIAL_DETAILs.Where(n => n.ORDERMATERIAL_ID == orderMaterialId).ToList();
        }
        public virtual ORDERMATERIAL_DETAIL GetById(int id)
        {
            try
            {
                return this.db.ORDERMATERIAL_DETAILs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<ORDERMATERIAL_DETAIL> GetAll()
        {
            return this.db.ORDERMATERIAL_DETAILs.ToList();
        }
        public virtual void Create(ORDERMATERIAL_DETAIL user)
        {
            try
            {
                this.db.ORDERMATERIAL_DETAILs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(ORDERMATERIAL_DETAIL user)
        {
            try
            {
                ORDERMATERIAL_DETAIL userOld = this.GetById(user.ID);
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
                ORDERMATERIAL_DETAIL user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(ORDERMATERIAL_DETAIL user)
        {
            try
            {
                db.ORDERMATERIAL_DETAILs.DeleteOnSubmit(user);
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
                ORDERMATERIAL_DETAIL user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(ORDERMATERIAL_DETAIL user)
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