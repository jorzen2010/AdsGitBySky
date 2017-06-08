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
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static string GetBabyNameById(int id)
        {
            return unitOfWork.adsBabysRepository.GetByID(id).BabyName;
        }
    }
}
