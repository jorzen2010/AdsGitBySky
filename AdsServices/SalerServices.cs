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
    public class SalerServices
    {
        private static  UnitOfWork unitOfWork = new UnitOfWork();

        public static string GetSalerName(int id)
        {
            
            var salers = unitOfWork.adsSalerRepository.Get(filter: u => u.SalerCode == id);

            if (salers.Count() > 0)
            {
                return salers.First().SalerName;

            }
            else
            {
                return "此推荐码不存在";
            
            }

            
        
        }
    }
}
