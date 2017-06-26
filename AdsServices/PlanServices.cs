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

                    if (a<b)
                    {
                        string empty = sArray[j];
                        sArray[j] = sArray[j + 1];
                        sArray[j + 1] = empty;
                    }
                }
            }

            //应该直接指定出来计划列表以供参考

            string er = CommonTools.arrayToString(sArray);
            return er;
        }

        public  static List<BaogaoDemention> PlanCategory(int bid)
        {
            Baogao baogao = BaogaoServices.GetFirstBaogaoByBabyID(bid);
            List<BaogaoDemention> demlist = new List<BaogaoDemention>();


            if (string.IsNullOrEmpty(baogao.BaogaoWeight))
            {
                return demlist;
            }
            else
            {
                string x = baogao.BaogaoWeight;




                // string x = "感觉能力:99,交往能力:56,运动能力:46,语言能力:76,自理能力:90";



                string[] sArray = x.Split(',');

                int number = 0;
                foreach (string s in sArray)
                {
                    number++;
                    // string  dem=s.ToString();

                    BaogaoDemention dem = new BaogaoDemention();
                    dem.demName = s.Substring(0, s.IndexOf(":"));
                  //  dem.demScore = int.Parse(s.Substring(s.IndexOf(":") + 1));
                    //Category category = (from c in db.Categorys
                    //                     orderby c.CategorySort ascending
                    //                     where c.CategoryName == dem.demName
                    //                     select c).First();
                    Category category = unitOfWork.categorysRepository.Get(filter: u => u.CategoryName == dem.demName).First();


                    dem.demNumber = number;
                    dem.demIcon = category.CategoryIcon;
                    dem.demcategoryid = category.ID;

                    demlist.Add(dem);

                }

                return demlist;
            
            }

           
        
        }




        public static List<PlanOrderWeight> MakePlanByScore(string dem, int babyid)
        {
            List<PlanOrderWeight> orderlist=new List<PlanOrderWeight>();
            int categoryid = unitOfWork.categorysRepository.Get(filter: u => u.CategoryName == dem).First().ID;

            var videos = unitOfWork.adsVideosRepository.Get(filter: v => v.VideoCategory == categoryid);

            if (videos.Count() > 0)
            {
                foreach (AdsVideo video in videos)
                {
                    float pingjiastatus = 0.5f;

                    var pingjias = unitOfWork.pingjiasRepository.Get(filter: v => v.VideoId == video.VideoId && v.BabyId==babyid);
                    if (pingjias.Count() > 0)
                    {
                        pingjiastatus = pingjias.First().PingjiaValue;
                    }
                    PlanOrderWeight planOrderWeight = new PlanOrderWeight();
                    planOrderWeight.VideoId = video.VideoId;
                    planOrderWeight.OrderValue = (video.VideoWeight * pingjiastatus) * (video.VideoWeight * pingjiastatus);
                    orderlist.Add(planOrderWeight);

                }

              //  peoples.Sort( (a, b) => a.age.CompareTo(b.age) );
              //  peoples.Sort(delegate (People p1, People p2) { return p1.age.CompareTo(p2.age); });

                orderlist.Sort((a, b) => a.OrderValue.CompareTo(b.OrderValue));

                return orderlist;

            }
            else
            {
                return orderlist;
                //有错误
            
            }

                
        
        }

    }
}
