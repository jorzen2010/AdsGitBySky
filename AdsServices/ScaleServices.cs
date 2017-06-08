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
    public class ScaleServices
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static string GetScaleNameById(int id)
        {
            return unitOfWork.scaleRepository.GetByID(id).ScaleName;
        }
    }
}
