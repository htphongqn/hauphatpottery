using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hauphatpottery.Data
{
    public class UnitDataRepo
    {
        private hauphatpotteryDataContext db = new hauphatpotteryDataContext();

        public bool checkPermissionPage(string pageName, int groupId, int groupTypeId)
        {
            if (groupTypeId == 1)
                return true;
            var list = (from a in db.MENU_PARENTs
                        join b in db.GROUP_MENUs on a.MENU_PAR_ID equals b.MENU_ID
                        where b.GROUP_ID == groupId
                        && a.MENU_PARENT_LINK == pageName
                        select a);
            if (list != null && list.ToList().Count > 0)
                return true;
            return false;
        }
    }
}