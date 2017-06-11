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
    public class PlanServices
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static string MakePlan(string sacleresult)
        {
            

            string[] sArray = sacleresult.Split(',');



            for (int i = 0; i < sArray.Length - 1; i++)
            {
              
                for (int j = 0; j < sArray.Length - i - 1; j++)
                {
                    float a = float.Parse(sArray[j].Substring(sArray[j].IndexOf(":") + 1));
                    float b = float.Parse(sArray[j + 1].Substring(sArray[j + 1].IndexOf(":") + 1));

                    if (a>b)
                    {
                        string empty = sArray[j];
                        sArray[j] = sArray[j + 1];
                        sArray[j + 1] = empty;
                    }
                }
            }

            for (int k = 0; k < sArray.Length - 1; k++)
            {

            }
            string er = CommonTools.arrayToString(sArray);
            return er;
        }

        public string demplan(string dem,int count)
        {
            string demplan = "";

            int  categoryid = unitOfWork.categorysRepository.Get(filter: u => u.CategoryName == dem).First().ID;

            var program = unitOfWork.adsVideosRepository.Get(filter: u => u.VideoCategory == categoryid && u.VideoWeight==0.3);




            return demplan;
        
        }

    }
}
