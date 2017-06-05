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
        public ActionResult Calendar(int? bid)
        {
            int babyid = bid ??1;
            AdsBaby baby = new AdsBaby();
            var babys = unitOfWork.adsBabysRepository.Get(filter: u => u.CustomerId == bid);
            int count = babys.Count();
            if (count > 0)
            { 
                baby = babys.First() as AdsBaby;
            }
            

            if (Session["nickname"] == null)
            {
                ViewBag.info = "你尚未登录无法查看";
                return View(baby);
            }
            else
            {
                if (string.IsNullOrEmpty(baby.BabyName))
                {
                    ViewBag.info = "你尚未开通训练计划";
                    return View(baby);
                }
                else
                {
                    return View(baby);
                }
            
            }

                

           // return View();
        }

        public ActionResult Login()
        {
         //   string nickname = Session["nickname"].ToString();
            if (Session["nickname"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Calendar","Wechat");
            }
        
        }

       
        public ActionResult WechatLogin()
        {

                string userAgent = Request.UserAgent;
         
                WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();

                string REDIRECT_URI = System.Web.HttpUtility.UrlEncode("http://wx.zzd123.com/Wechat/MemberCenter");

                string SCOPE = "snsapi_userinfo";

                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + wechatconfig.Appid + "&redirect_uri=" + REDIRECT_URI + "&response_type=code&scope=" + SCOPE + "&state=STATE#wechat_redirect";

                return Redirect(url);
           


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
            if (Session["nickname"] == null)
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
                    customer.CustomerBirthday = System.DateTime.Now;
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

                Session["nickname"] = customer.CustomerNickName;
                Session["openid"] = customer.CustomerOpenid;
                Session["cid"] = customer.CustomerId;

                return View(customer);
            }
            else
            {
                string nickname = Session["nickname"].ToString();
                string openid = Session["openid"].ToString();
                int cid = int.Parse(Session["cid"].ToString());
                AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(cid);
                return View(customer);
            }

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
