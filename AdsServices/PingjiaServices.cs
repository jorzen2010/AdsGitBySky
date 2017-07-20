using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Common;
using AdsEntity;
using AdsDal;


namespace AdsServices
{
    public class PingjiaServices
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();
       
      
        public static bool  GetPingjiaCount(int vid,int bid,DateTime dt)
        {

            var  pingjias = unitOfWork.pingjiasRepository.Get(filter: u => u.VideoId == vid &&u.BabyId==bid , orderBy: q => q.OrderByDescending(u => u.PingjiaId));

            if (pingjias.Count() == 0)
            {
                return true;
            }

            else
            {
                DateTime preTime = pingjias.First().PingjiaTime;

                TimeSpan ts=dt-preTime;
                if(ts.TotalHours>6)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                 
            
            }

            

        }


      
        
    }
}
