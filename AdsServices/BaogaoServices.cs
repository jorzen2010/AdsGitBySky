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
    public class BaogaoServices
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static string GetBabyNameById(int id)
        {
            return unitOfWork.adsBabysRepository.GetByID(id).BabyName;
        }

        public static List<Baogao> GetBaogaoListByBabyID(int bid)
        {
            var baogaos = unitOfWork.baogaoRepository.Get(filter: u => u.BabyId == bid);
            List<Baogao> BaogaoList = new List<Baogao>();
            foreach (Baogao baogao in baogaos)
            {
                BaogaoList.Add(baogao);
            }
            return BaogaoList;
        }

        public static Baogao GetFirstBaogaoByBabyID(int bid)
        {
            Baogao baogao = new Baogao();
            var baogaos = unitOfWork.baogaoRepository.Get(filter: u => u.BabyId == bid,orderBy: q =>q.OrderByDescending(u=>u.BaogaoId));
            if (baogaos.Count() > 0)
            {
                baogao = baogaos.First();

            }
            return baogao;

            
        }
    }
}
