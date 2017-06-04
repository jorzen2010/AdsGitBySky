using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using AdsEntity.WechatEntity;
using AdsWeb.WechatServices;
using Common;

namespace AdsWeb.Controllers
{
    public class WxController : Controller
    {

        public ActionResult Index()
        {

            string access_token = AccessTokenService.GetAccessToken();
            WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();
            string xml = XmlSerializerHelper.ToXml(wechatconfig);


            ViewBag.Message = access_token;
            ViewBag.Message2 = xml;

            return View();
        }

        public ActionResult About()
        {
            WechatTemplateMessage msgData = new WechatTemplateMessage
            {
                touser = "oChqYt1b0t1U2_b_U9ag1JQyrebM",
                template_id = "KqDQm6LejvmNDrTWIQuRYoflIpi7JKgvUNsQfqf8i70",
                url = "http://www.baidu.com",
                data = new
                {

                    welcome = new
                    {
                        value = "训练提醒通知",
                        color = "#173177"
                    },
                    first = new
                    {
                        value = "2015年6月7日",
                        color = "#173177"
                    },
                    second = new
                    {
                        value = "踢皮球",
                        color = "#173177"
                    },
                }
            };







            string access_token = AccessTokenService.GetAccessToken();
            string operate = "SendTemplateMessage";
            string postdata = JsonConvert.SerializeObject(msgData);

            string result = WechatService.wechatApi(operate, access_token, postdata);

            ViewBag.Message = JsonConvert.SerializeObject(msgData);
            ViewBag.result = result;
            return View();
        }

        public ActionResult Contact()
        {
            UserListItem[] u = new UserListItem[2];
            u[0] = new UserListItem { openid = "oChqYt13RL8dBi0zKHo0kao-aSHo", lang = "zh_CN" };
            u[1] = new UserListItem { openid = "oChqYt1b0t1U2_b_U9ag1JQyrebM", lang = "zh_CN" };

            UserList ulist = new UserList
            {
                user_list = u
            };



            string postdata = JsonConvert.SerializeObject(ulist);
            string access_token = AccessTokenService.GetAccessToken();
            string operate = "GetUserListInfo";

            string result = WechatService.wechatApi(operate, access_token, postdata);

            ViewBag.Message = postdata;
            ViewBag.result = result;

            return View();
        }

        public ActionResult Jstest()
        {
            string userAgent = Request.UserAgent;
            string access_token = AccessTokenService.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            //  result = HttpWebResponseUtility.CreateGetHttpResponse(url,null,userAgent,null);


            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();
            WechatJsTicket ticket = JsonConvert.DeserializeObject<WechatJsTicket>(result);

            string jsapi_ticket = ticket.ticket;
            string url2 = Request.Url.ToString();

            string string1 = "jsapi_ticket=" + jsapi_ticket +
                  "&noncestr=" + "zhaozheng" +
                  "&timestamp=" + "1414587457" +
                  "&url=" + url2;
            string x = SkyEncrypt.SHA1(string1);
            ViewBag.signature = x;
            return View();

        }

        public ActionResult js()
        {
            string userAgent = Request.UserAgent;
            string access_token = AccessTokenService.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            //  result = HttpWebResponseUtility.CreateGetHttpResponse(url,null,userAgent,null);


            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();
            WechatJsTicket ticket = JsonConvert.DeserializeObject<WechatJsTicket>(result);

            string jsapi_ticket = ticket.ticket;
            string url2 = Request.Url.ToString();

            string string1 = "jsapi_ticket=" + jsapi_ticket +
                  "&noncestr=" + "zhaozhengo" +
                  "&timestamp=" + "1414587487" +
                  "&url=" + url2;
            string x = SkyEncrypt.SHA1(string1);
            ViewBag.signature = x;
            return View();

        }

        public ActionResult wechatjs()
        {

            string access_token = AccessTokenService.GetAccessToken();
            string userAgent = Request.UserAgent;
            WechatJsTicket jsapi_ticket = WechatJsServices.GetJsapi_ticket(access_token, userAgent);

            string timestamp = TimeHelp.GetTimeStamp(DateTime.Now, 10);
            string nonceStr = "zhaozheng";
            string currentUrl = Request.Url.ToString();
            // string currentUrl = "http://wx.zzd123.com/home/wechatjs";
            string signature = WechatJsServices.GetSignature(jsapi_ticket.ticket, nonceStr, timestamp, currentUrl);

            WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();


            ViewBag.timestamp = int.Parse(timestamp);
            ViewBag.nonceStr = nonceStr;
            ViewBag.appid = wechatconfig.Appid;
            ViewBag.signature = signature;
            ViewBag.jsapi_ticket = jsapi_ticket.ticket;
            ViewBag.currentUrl = currentUrl;



            return View();
        }

        public ActionResult GetUserInfo()
        {
            WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();

            string REDIRECT_URI = System.Web.HttpUtility.UrlEncode("http://wx.zzd123.com/Home/GetUserId");

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
        public ActionResult GetUserInfoByopenid(string openid)
        {
            if (string.IsNullOrEmpty(openid))
            {
                openid = "oChqYt13RL8dBi0zKHo0kao-aSHo";
            }
            string userAgent = Request.UserAgent;
            string access_token = AccessTokenService.GetAccessToken();
            WechatUserInfo userinfo = new WechatUserInfo();
            userinfo = WechatApi.GetUserInfo(access_token, userAgent, openid);

            return View(userinfo);
        }

       
    }
}
