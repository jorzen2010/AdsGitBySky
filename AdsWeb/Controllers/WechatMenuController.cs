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
            MenuInfo menu2 = new MenuInfo("免费体验", new MenuInfo[] { 
                new MenuInfo("免费行为测评", MenuInfo.ButtonType.view, "http://wx.zzd123.com/Free/FreeScale"),
                new MenuInfo("免费心理服务", MenuInfo.ButtonType.view, "http://wx.zzd123.com/Free/FreeHeartList")
            });
            //  MenuInfo menu2 = new MenuInfo("行为测评", MenuInfo.ButtonType.view, "http://www.sina.com");
            //MenuInfo relatedInfo = new MenuInfo("相关链接", new MenuInfo[] { 
            //    new MenuInfo("公司介绍", MenuInfo.ButtonType.click, "Event_Company"),
            //    new MenuInfo("官方网站", MenuInfo.ButtonType.view, "http://www.iqidi.com"),
            //    new MenuInfo("提点建议", MenuInfo.ButtonType.click, "Event_Suggestion"),
            //    new MenuInfo("联系客服", MenuInfo.ButtonType.click, "Event_Contact"),
            //    new MenuInfo("发邮件", MenuInfo.ButtonType.view, "http://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=S31yfX15fn8LOjplKCQm")
            //});

            MenuJson menujson = new MenuJson();
            menujson.button.AddRange(new MenuInfo[] { menu,menu2});

            string token = AccessTokenService.GetAccessToken();

            string postData = JsonConvert.SerializeObject(menujson);

            ViewBag.x= WechatService.wechatApi("CreateMenu",token,postData);
            

            return View();
        }

        public ActionResult CreateMenuold()
        {


            MenuInfo menu = new MenuInfo("开始训练", MenuInfo.ButtonType.view, "http://wx.zzd123.com/wechat/Calendar");
            MenuInfo menu2 = new MenuInfo("免费体验", new MenuInfo[] { 
                new MenuInfo("免费行为测评", MenuInfo.ButtonType.view, "http://wx.zzd123.com/Free/FreeScale"),
                new MenuInfo("免费心理服务", MenuInfo.ButtonType.view, "http://wx.zzd123.com/Free/FreeHeartList")
            });
            MenuJson menujson = new MenuJson();
            menujson.button.AddRange(new MenuInfo[] { menu,menu2 });

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




        public ActionResult CreateWechatMenu()
        {

            return View();

        }

        [HttpPost]
        public ActionResult CreateMenu()
        {
            string postData = Request.Form["content"].Replace("\r\n", "").Replace("\\", "");
            string token = AccessTokenService.GetAccessToken();

            string result = WechatMenuServices.CreateMenu(token, postData);
            WechatResult wechatResult = JsonConvert.DeserializeObject<WechatResult>(result);
            if (wechatResult.errcode == 0)
            {
                ViewBag.msg = "微信菜单创建成功！菜单代码如下：";
                ViewBag.result = postData;
            }
            else
            {
                ViewBag.msg = "微信菜单创建失败！错误代码如下：";
                ViewBag.result = result;

            }

            return View();

        }

        public ActionResult GetWechatMenu()
        {
            string token = AccessTokenService.GetAccessToken();
            string menuStr = WechatMenuServices.GetMenu(token);
            WechatResult wechatResult = JsonConvert.DeserializeObject<WechatResult>(menuStr);
            if (wechatResult.errcode != 0)
            {
                ViewBag.msg = "此公众号目前没有菜单！返回错误代码如下：";
            }
            else
            {
                ViewBag.msg = "成功获取到菜单内容！菜单代码如下：";
            }
            ViewBag.content = menuStr;

            return View();

        }

        public ActionResult DeleteWechatMenu()
        {
            string token = AccessTokenService.GetAccessToken();
            string result = WechatMenuServices.DeleteMenu(token);
            ViewBag.result = result;
            WechatResult wechatResult = JsonConvert.DeserializeObject<WechatResult>(result);
            if (wechatResult.errcode == 0)
            {
                ViewBag.msg = "删除成功！";
            }
            else
            {
                ViewBag.msg = "删除失败！";

            }
            return View();

        }

    
    }
}
