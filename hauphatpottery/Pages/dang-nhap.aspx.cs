using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using hauphatpottery.Components;
using hauphatpottery.Data;

namespace hauphatpottery.Pages
{
    public partial class dang_nhap : System.Web.UI.Page
    {
        #region Declare

        hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region My Function

        private void LogOut()
        {
            try
            {
                Session.Abandon();
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void LogIn()
        {
            try
            {
                int id = Log_In(txtUsername.Value, txtPassword.Value);
                if (id == 10)
                {
                    Load_All_Cus(txtUsername.Value);
                    Response.Redirect("trang-chu.aspx", false);
                }
                else if (id == 3)
                {
                    clsDataUtil.Show("Tài khoản chưa kích hoạt!");
                }
                else if (id == 1 || id == 2)
                {
                    clsDataUtil.Show("Tên đăng nhập hoặc mập khẩu không chính xác!");
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public int Log_In(string Username, string MatKhau)
        {
            try
            {
                var _vLogin = db.GetTable<USER>().Where(a => a.USER_UN == Username);
                if (_vLogin.ToList().Count > 0)
                {
                    if (Common.Encrypt(MatKhau, _vLogin.First().SALT) == _vLogin.First().USER_PW)
                    {
                        if (_vLogin.First().USER_ACTIVE == 1)
                        {
                            return 10;//dang nhap thanh cong
                        }
                        else
                        {
                            return 3;//chua kich hoat
                        }
                    }
                    else
                    {
                        return 2;// sai pass
                    }
                }
                else
                {
                    return 1;//1 sai username
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return 0;//lỗi
            }
        }

        public void Load_All_Cus(string Username)
        {
            try
            {
                var _cus = db.GetTable<USER>().Where(a => a.USER_UN == Username);
                if (_cus.ToList().Count > 0)
                {
                    Session["Userid"] = _cus.First().USER_ID;
                    Session["Name"] = _cus.First().USER_NAME;
                    Session["Groupid"] = _cus.First().GROUP_ID;
                    var gettype = db.GROUPs.Where(n => n.GROUP_ID == _cus.First().GROUP_ID).ToList();
                    if (gettype.Count > 0)
                    {
                        Session["Grouptype"] = gettype.First().GROUP_TYPE;
                    }
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion

        #region Button Handler

        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            LogIn();
        }
        #endregion
    }
}