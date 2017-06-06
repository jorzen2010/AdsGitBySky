using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using AdsEntity;
using AdsEntity.WechatEntity;
using AdsWeb.WechatServices;
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
             string userAgent = Request.UserAgent;

            string CODE = Request["code"];

            string STATE = Request["state"];

            WebchatJsUserinfo userinfo = WechatJsServices.GetUserInfo(userAgent, CODE);

            var cus = unitOfWork.adsCustomersRepository.Get(filter: u => u.CustomerOpenid == userinfo.openid);
            int aa = cus.Count();
            AdsCustomer customer = new AdsCustomer();
            if (aa == 0)
            {

                customer.CustomerOpenid = userinfo.openid;
                customer.CustomerNickName = userinfo.nickname;
                if (userinfo.sex == 0)
                {
                    customer.CustomerSex = "未知";
                }
                if (userinfo.sex == 1)
                {
                    customer.CustomerSex = "男";
                }
                if (userinfo.sex == 2)
                {
                    customer.CustomerSex = "女";
                }
                customer.CustomerUnionid = userinfo.unionid;
                customer.CustomerRole = 4;
                customer.CustomerStatus = false;
                customer.CustomerIdentity = AdsCustomer.IdentiyStatus.未申请计划;
                customer.CustomerLastLoginTime = System.DateTime.Now;
                customer.CustomerRegTime = System.DateTime.Now;

                customer.CustomerAvatar = userinfo.headimgurl;
                customer.CustomerUserName = userinfo.openid;
                customer.CustomerProvince = userinfo.province;
                customer.CustomerCity = userinfo.city;

                unitOfWork.adsCustomersRepository.Insert(customer);
                unitOfWork.Save();
            }
            else
            {
                customer = cus.First() as AdsCustomer;
            }

            Session["CustomerNickName"] = customer.CustomerNickName;
            Session["CustomerOpenid"] = customer.CustomerOpenid;
            Session["CustomerId"] = customer.CustomerId;

            return Redirect(STATE);
                

           // return View();
        }

        public ActionResult Calendar()
        {
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    return View(baby);
                }
                else
                {
                    ViewBag.info = "您尚未开通训练计划";
                    return View(baby);

                }

            }
            else
            {
                ViewBag.info = "您尚未登录";
                return View(baby);
            
            }
        }

        public ActionResult Login()
        {
         //   string nickname = Session["nickname"].ToString();
            if (Session["CustomerNickName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("MemberCenter", "Wechat");
            }
        
        }

       
        public ActionResult WechatLogin()
        {
                string sourceUrl = Request.UrlReferrer.ToString();

                string userAgent = Request.UserAgent;
         
                WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();

                string REDIRECT_URI = System.Web.HttpUtility.UrlEncode("http://wx.zzd123.com/Wechat/Index");

                string SCOPE = "snsapi_userinfo";
                string STATE = sourceUrl;

                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + wechatconfig.Appid + "&redirect_uri=" + REDIRECT_URI + "&response_type=code&scope=" + SCOPE + "&state="+STATE+"#wechat_redirect";

                return Redirect(url);
           


        }

       
      

        public ActionResult Ceping()
        {
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    return View(baby);
                }
                else
                {
                    ViewBag.info = "您尚未开通训练计划";
                    return View(baby);

                }

            }
            else
            {
                ViewBag.info = "您尚未登录";
                return View(baby);

            }
        }

        public ActionResult HeartList()
        {
            return View();
        }

        public ActionResult MemberCenter()
        {
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
                AdsBaby baby = new AdsBaby();
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    ViewBag.baby = true;
                }
                else
                {
                    ViewBag.baby = false;
                
                }
                return View(customer);

            }

            else
            {
                return RedirectToAction("Login");
            
            }
           
               
            

        }

        public ActionResult Baogao()
        {
            AdsBaby baby = new AdsBaby();
            if (Session["CustomerId"] != null)
            {
                int id = int.Parse(Session["CustomerId"].ToString());
                var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == id);
                int count = babys.Count();
                if (count > 0)
                {
                    baby = babys.First() as AdsBaby;
                    return View(baby);
                }
                else
                {
                    ViewBag.info = "您尚未开通训练计划";
                    return View(baby);

                }

            }
            else
            {
                ViewBag.info = "您尚未登录";
                return View(baby);

            }
        }

        public ActionResult Scale()
        {
            return View();
        }

        public ActionResult Reg()
        {
            return View();
        }



        public ActionResult GetUserInfo()
        {
            WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();

            string REDIRECT_URI = System.Web.HttpUtility.UrlEncode("http://wx.zzd123.com/Wechat/GetUserId");

            string SCOPE = "snsapi_userinfo";

            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + wechatconfig.Appid + "&redirect_uri=" + REDIRECT_URI + "&response_type=code&scope=" + SCOPE + "&state=STATE#wechat_redirect";

            return Redirect(url);

        }
        public ActionResult GetUserId()
        {

            string CODE = Request["code"];
            string STATE = Request["state"];

            string userAgent = Request.UserAgent;

            WebchatJsUserinfo userinfo = WechatJsServices.GetUserInfo(userAgent, CODE);

            return View(userinfo);


        }




        public ActionResult Logout()
        {
            Session["CustomerNickName"] = null;
            Session["CustomerOpenid"] = null;
            Session["CustomerId"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult StarBaby()
        {
            if (Session["CustomerId"] != null)
            {
                ViewData["customerid"] = Session["CustomerId"].ToString();
                return View();
            }
            else {
                return RedirectToAction("Login");
            
            }
           
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BabyCreate(AdsBaby baby)
        {
            if (ModelState.IsValid)
            {
                
                baby.BabyAvatar = "~/wechat/img/star.jpg";
                baby.BabyRegTime = System.DateTime.Now;
                baby.BabyExpiredTime = System.DateTime.Now.AddYears(5);

                unitOfWork.adsBabysRepository.Insert(baby);
                unitOfWork.Save();

                return RedirectToAction("Calendar");
            }

            return View(baby);
        }

        //报告详情页
        public ActionResult BaogaoDetail(int id)
        {
            return View();
        }



        [HttpPost]

        public JsonResult SaveScaleResult(int id, string score, string Dementionscore)
        {
            Message msg = new Message();

            Baogao baogao = new Baogao();
            baogao.BaogaoScore = score;
            baogao.BaogaoDementionScore = Dementionscore;
            baogao.CustomerId = 1;
            baogao.ScaleId = 1;
            baogao.BaogaoTime = System.DateTime.Now;

            try
            {
                unitOfWork.baogaoRepository.Insert(baogao);
                unitOfWork.Save();
                msg.MessageStatus = "true";
                msg.MessageInfo = "保存成功" + score + Dementionscore;
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
