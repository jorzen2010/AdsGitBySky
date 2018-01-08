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
        public static IList<AdsCustomer> GetListForPageList(Pager pager)
        {
            pager = CommonDal.GetPager(pager);
            IList<AdsCustomer> customerList = DataConvertHelper<AdsCustomer>.ConvertToModel(pager.EntityDataTable);
            return customerList;
        }

        public static string GetCustomerName(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            string cname = string.Empty;
            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
            if (customer == null)
            {
                cname = "查无此人";
            }
            else
            {
                cname=  unitOfWork.adsCustomersRepository.GetByID(id).CustomerNickName;
            }
            unitOfWork.Dispose();
            return cname;
        
        }
    }
}
