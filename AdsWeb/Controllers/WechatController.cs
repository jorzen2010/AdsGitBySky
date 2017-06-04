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
            string CODE = Request["code"];
            string STATE = Request["state"];

            string userAgent = Request.UserAgent;

            WebchatJsUserinfo userinfo = WechatJsServices.GetUserInfo(userAgent, CODE);

            var cus = unitOfWork.adsCustomersRepository.Get(filter: u => u.CustomerOpenid == userinfo.openid);
            int aa = cus.Count();

            if (aa==0)
            {
                AdsCustomer customer = new AdsCustomer();
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
                customer.CustomerBirthday = System.DateTime.Now;
                customer.CustomerAvatar = userinfo.headimgurl;
                customer.CustomerUserName = userinfo.openid;
                customer.CustomerProvince = userinfo.province;
                customer.CustomerCity = userinfo.city;



               // customer.CustomerStatus=
                unitOfWork.adsCustomersRepository.Insert(customer);
                unitOfWork.Save();
            }
            else
            {

               
            
            }

            return View(userinfo);
           // return View();
        }

        public ActionResult Login()
        {
            WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();

            string REDIRECT_URI = System.Web.HttpUtility.UrlEncode("http://wx.zzd123.com/Wechat/Index");

            string SCOPE = "snsapi_userinfo";

            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + wechatconfig.Appid + "&redirect_uri=" + REDIRECT_URI + "&response_type=code&scope=" + SCOPE + "&state=STATE#wechat_redirect";

            return Redirect(url);
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
            return RedirectToAction("Login");
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult StarBaby()
        {
            return View();
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
