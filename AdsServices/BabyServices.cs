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
    public class BabyServices
    {
       

        public static string GetBabyNameById(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            string bname = string.Empty;
            AdsBaby baby = unitOfWork.adsBabysRepository.GetByID(id);
            if (baby == null)
            {
                bname = "查无此人";

            }
            else
            {
                bname = unitOfWork.adsBabysRepository.GetByID(id).BabyName;
            }
            unitOfWork.Dispose();
            return bname;
        }
    }
}
