using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AdsEntity;
using AdsDal;


namespace AdsServices
{
    public class CustomerServices
    {
        private static  UnitOfWork unitOfWork = new UnitOfWork();
        public static IList<AdsCustomer> GetListForPageList(Pager pager)
        {
            pager = CommonDal.GetPager(pager);
            IList<AdsCustomer> customerList = DataConvertHelper<AdsCustomer>.ConvertToModel(pager.EntityDataTable);
            return customerList;
        }

        public static string GetCustomerName(int id)
        {
            return  unitOfWork.adsCustomersRepository.GetByID(id).CustomerNickName;
        
        }
    }
}
