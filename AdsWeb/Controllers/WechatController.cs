using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using AdsEntity;
using AdsDal;

namespace AdsWeb.Controllers
{
    public class WechatController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        //首页/
        public ActionResult Index()
        {
            return View();
        }


       
        public ActionResult Calendar()
        {
            return View();  
        }

        public ActionResult Ceping()
        {
            return View();
        }

        public ActionResult HeartList()
        {
            return View();
        }

        public ActionResult MemberCenter()
        {
            return View();
        }

        public ActionResult Baogao()
        {
            return View();
        }

        public ActionResult Scale()
        {
            return View();
        }

        public ActionResult Reg()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        //报告详情页
        public ActionResult BaogaoDetail(int id)
        {
            return View();
        }



        [HttpPost]

        public JsonResult SaveScaleResult(int id)
        {
            Message msg = new Message();

            Baogao baogao = new Baogao();

            try
            {
                unitOfWork.baogaoRepository.Insert(baogao);
                msg.MessageStatus = "true";
                msg.MessageInfo = "保存成功";
            }
            catch
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "保存失败";
            
            }
            
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


    }
}
