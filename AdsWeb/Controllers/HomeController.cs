using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdsEntity;
using AdsDal;

namespace AdsWeb.Controllers
{
    public class HomeController : Controller
    { private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult zhaozheng()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            int id = 1;
            Setting setting = unitOfWork.settingsRepository.GetByID(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}