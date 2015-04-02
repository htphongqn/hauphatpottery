using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Components
{
    public class Cost
    {    
        //LOẠI NHÓM
        public const int GROUPTYPE_ADMIN = 1;
        public const int GROUPTYPE_EDITOR = 2;
        //Đường dẫn
        public const string PRODUCTPATH = "/Resource/Product/";
        //---Trạng thái đơn hàng---
        public const int DANGCHO = 1;
        public const int SANXUAT = 2;
        public const int HOANTAT = 3;
        //INVENTORY TYPE
        public const int NHAP_THO = 1;
        public const int NHAP_TINH = 2;
        public const int XUAT_SANPHAM = 3;
        //SHAPE HÌNH DẠNG: Tròn D H, Chu nhat L W H, Vuong W H
    }
}