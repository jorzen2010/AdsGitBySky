using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AdsEntity;
using AdsDal;

namespace AdsWeb.Controllers
{
    public class AgeTools
    {
        private SkyWebContext db = new SkyWebContext();
        public int GetCountByBirthDay(DateTime dt, int age)
        {


            if (age == 3)
            {
                var babys = from s in db.AdsBabys
                            where (dt.Year - s.BabyBirthday.Value.Year <= age)
                            select s;
                return babys.Count();

            }
            else if (age == 12)
            {
                var babys = from s in db.AdsBabys
                            where (dt.Year - s.BabyBirthday.Value.Year >= age)
                            select s;
                return babys.Count();

            }
            else
            {
                var babys = from s in db.AdsBabys
                            where (dt.Year - s.BabyBirthday.Value.Year == age)
                            select s;
                return babys.Count();
            }
        }
           
    }
}