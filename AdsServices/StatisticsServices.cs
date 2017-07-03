﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AdsEntity;
using AdsDal;


namespace AdsServices
{
    public class StatisticsServices
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();
        public static int GetDaysByBabyId(int id)
        {
            return 0;
        
        }

        public static string GetProgramNameById(int id)
        {
            return unitOfWork.adsVideosRepository.GetByID(id).VideoName;
        }


        public static int GetCepingCountByBabyId(int id)
        {
          return   unitOfWork.baogaoRepository.Get().Count();
        
        }

        public static int GetPingjiaCountByBabyId(int id,float cid)
        {
            int count = 0;
            if (cid == 0)
            {
                count = unitOfWork.pingjiasRepository.Get(filter: u => u.BabyId == id).Count();
            }
            if (cid>0)
            {
                count = unitOfWork.pingjiasRepository.Get(filter: u => u.BabyId == id && u.PingjiaValue==cid).Count();
            }
           
            return count;
        }

        public static int  GetDaysByCustomerId(DateTime BabyRegTime)
        {
            DateTime dt1 = DateTime.Now;
            int days = (dt1 - BabyRegTime).Days;
            return days;
 
        }
        
    }
}