using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using AdsEntity;
using AdsDal;
using AdsServices;
using Common;

namespace AdsWeb.Controllers
{
    public class TongjiController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SkyWebContext db = new SkyWebContext();
        public ActionResult BabyTongji()
        {

            int Boy = unitOfWork.adsBabysRepository.Get(filter: u => u.BabySex == "男").Count();
            int Girl = unitOfWork.adsBabysRepository.Get(filter: u => u.BabySex == "女").Count();
            int Total = unitOfWork.adsBabysRepository.Get().Count();

         
            ViewBag.Boys = Boy;
            ViewBag.Girls = Girl;

            DateTime dt=DateTime.Now;

            AgeTools ageTools = new AgeTools();

            ViewBag.age3 = ageTools.GetCountByBirthDay(dt,3);
            ViewBag.age12 = ageTools.GetCountByBirthDay(dt, 12);
            ViewBag.age4 = ageTools.GetCountByBirthDay(dt, 4);
            ViewBag.age5 = ageTools.GetCountByBirthDay(dt, 5);
            ViewBag.age6 = ageTools.GetCountByBirthDay(dt, 6);
            ViewBag.age7 = ageTools.GetCountByBirthDay(dt, 7);
            ViewBag.age8 = ageTools.GetCountByBirthDay(dt, 8);
            ViewBag.age9 = ageTools.GetCountByBirthDay(dt, 9);
            ViewBag.age10 = ageTools.GetCountByBirthDay(dt, 10);
            ViewBag.age11 = ageTools.GetCountByBirthDay(dt, 11);
           
           

            return View();
        }

        public ActionResult CategoryTongji(int bid)
        {

            string A1="", B1="", C1="",catName = "";
            

            //这里需要循环Category
            List<Category> listcat= CategoryServices.GetCategoryListByParentID(3);

          


            if (bid == 0)
            {
                foreach (Category cat in listcat)
                {
                    catName = catName +" \'"+cat.CategoryName + "\'" + ",";
                    A1 = A1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.PingjiaValue == 0.3f).Count().ToString() + ",";
                    B1 = B1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.PingjiaValue == 0.5f).Count().ToString() + ",";
                    C1 = C1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.PingjiaValue == 0.8f).Count().ToString() + ",";


                }
                catName = @catName.TrimEnd(',');
                A1 = A1.TrimEnd(',');
                B1 = B1.TrimEnd(',');
                C1 = C1.TrimEnd(',');

            }



            else
            {
                foreach (Category cat in listcat)
                {
                    catName = catName + "'" + cat.CategoryName + "'" + ",";
                  
                    A1 = A1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.BabyId == bid && u.PingjiaValue == 0.3f).Count().ToString() + ",";
                    B1 = B1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.BabyId == bid && u.PingjiaValue == 0.5f).Count().ToString() + ",";
                    C1 = C1 + unitOfWork.pingjiasRepository.Get(filter: u => u.VideoCategory == cat.ID && u.BabyId == bid && u.PingjiaValue == 0.8f).Count().ToString() + ",";


                }
                catName = catName.TrimEnd(',');
                A1 = A1.TrimEnd(',');
                B1 = B1.TrimEnd(',');
                C1 = C1.TrimEnd(',');


            }


          

            ViewBag.A = A1;
            ViewBag.B = B1;
            ViewBag.C = C1;
            ViewBag.catName = catName;


            return View();
        }

        public ActionResult ProgramTongji(int id)
        {

            int A, B, C;
            string VideoName = "";
            if (id == 0)
            {
                A = unitOfWork.pingjiasRepository.Get(filter: u => u.PingjiaValue == 0.3f).Count();
                B = unitOfWork.pingjiasRepository.Get(filter: u => u.PingjiaValue == 0.5f).Count();
                C = unitOfWork.pingjiasRepository.Get(filter: u => u.PingjiaValue == 0.8f).Count();
                VideoName = "全部";

            }
            else
            {
                A = unitOfWork.pingjiasRepository.Get(filter: u => u.VideoId == id && u.PingjiaValue==0.3f).Count();
                B = unitOfWork.pingjiasRepository.Get(filter: u => u.VideoId == id && u.PingjiaValue == 0.5f).Count();
                C = unitOfWork.pingjiasRepository.Get(filter: u => u.VideoId == id && u.PingjiaValue == 0.8f).Count();
                VideoName = VideoServices.GetVideoNameById(id);
            
            }


           int AA = unitOfWork.pingjiasRepository.Get(filter: u => u.PingjiaValue == 0.3f).Count();
           int BB = unitOfWork.pingjiasRepository.Get(filter: u => u.PingjiaValue == 0.5f).Count();
           int CC = unitOfWork.pingjiasRepository.Get(filter: u => u.PingjiaValue == 0.8f).Count();
           
            ViewBag.A = A;
            ViewBag.B = B;
            ViewBag.C = C;
            ViewBag.AA = AA;
            ViewBag.BB = BB;
            ViewBag.CC = CC;
            ViewBag.VideoName = VideoName;
            return View();
        }

        
    }
}
