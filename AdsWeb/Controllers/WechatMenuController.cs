using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdsEntity.WechatEntity;
using Common;
using Newtonsoft.Json;
using AdsWeb.WechatServices;

namespace AdsWeb.Controllers
{
    public class WechatMenuController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {


            MenuInfo menu = new MenuInfo("开始训练",MenuInfo.ButtonType.view, "http://wx.zzd123.com/wechat/Calendar");
            //  MenuInfo menu2 = new MenuInfo("行为测评", MenuInfo.ButtonType.view, "http://www.sina.com");
            //MenuInfo relatedInfo = new MenuInfo("相关链接", new MenuInfo[] { 
            //    new MenuInfo("公司介绍", MenuInfo.ButtonType.click, "Event_Company"),
            //    new MenuInfo("官方网站", MenuInfo.ButtonType.view, "http://www.iqidi.com"),
            //    new MenuInfo("提点建议", MenuInfo.ButtonType.click, "Event_Suggestion"),
            //    new MenuInfo("联系客服", MenuInfo.ButtonType.click, "Event_Contact"),
            //    new MenuInfo("发邮件", MenuInfo.ButtonType.view, "http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=S31yfX15fn8LOjplKCQm")
            //});

            MenuJson menujson = new MenuJson();
            menujson.button.AddRange(new MenuInfo[] { menu});

            string token = AccessTokenService.GetAccessToken();

            string postData = JsonConvert.SerializeObject(menujson);

            ViewBag.x= WechatService.wechatApi("CreateMenu",token,postData);
            

            return View();
        }

        public ActionResult CreateMenu()
        {


            MenuInfo menu = new MenuInfo("开始训练", MenuInfo.ButtonType.view, "http://wx.zzd123.com/wechat/Calendar");

            MenuJson menujson = new MenuJson();
            menujson.button.AddRange(new MenuInfo[] { menu });

            string token = AccessTokenService.GetAccessToken();

            string postData = JsonConvert.SerializeObject(menujson);

            ViewBag.x = WechatService.wechatApi("CreateMenu", token, postData);


            return View();
        }

        public ActionResult DeleteMenu()
        {
            string token = AccessTokenService.GetAccessToken();
            string result = WechatMenuServices.DeleteMenu(token);
            ViewBag.result = result;
            return View();
        }

        public ActionResult GetMenu()
        {
            string token = AccessTokenService.GetAccessToken();
            string result = WechatMenuServices.GetMenu(token);
            ViewBag.result = result;
            return View();
        }

    
    }
}
