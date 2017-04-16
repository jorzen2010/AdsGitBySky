using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AdsEntity;


namespace AdsServices
{
    public class CustomerServices
    {
        public static IList<AdsCustomer> GetListForPageList(Pager pager)
        {
            pager = CommonDal.GetPager(pager);
            IList<AdsCustomer> customerList = DataConvertHelper<AdsCustomer>.ConvertToModel(pager.EntityDataTable);
            return customerList;
        }
    }
}
