using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AdsEntity;


namespace AdsServices
{
    public class SysServices
    {
        public static IList<SysUser> GetListForPageList(Pager pager)
        {
            pager = CommonDal.GetPager(pager);
            IList<SysUser> sysUsers = DataConvertHelper<SysUser>.ConvertToModel(pager.EntityDataTable);
            return sysUsers;
        }
    }
}
