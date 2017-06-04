using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Configuration;
using Common;
using AdsEntity.WechatEntity;

namespace AdsWeb.WechatServices
{
    public class WechatJsServices
    {
        public static WechatJsTicket GetJsapi_ticket(string access_token, string userAgent)
        {

            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);

            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);

            //Stream stream = response.GetResponseStream();
            //StreamReader sr = new StreamReader(stream);
            //string result = sr.ReadToEnd();
            string result = HttpWebResponseUtility.HttpResponseToString(response);
            WechatJsTicket ticket = JsonConvert.DeserializeObject<WechatJsTicket>(result);

            return ticket;
       
        }

        public static string GetSignature(string jsapi_ticket, string nonceStr, string timestamp,string currentUrl)
        {

            string string1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi_ticket, nonceStr, timestamp, currentUrl);
      //      string string1 = "jsapi_ticket=" + jsapi_ticket +"&noncestr=" + "zhaozhengo" +"&timestamp=" + "1414587487" +"&url=" + currentUrl;
            return SkyEncrypt.SHA1(string1);

        }

        public static WechatJsConfig GetWechatConfig()
        {

            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");


            AppSettingsSection appsection = config.GetSection("appSettings") as AppSettingsSection;


            WechatJsConfig JsConfig = new WechatJsConfig();

            JsConfig.WechatJsUrl = appsection.Settings["WechatJsUrl"].Value;
            JsConfig.WechatJsToken = appsection.Settings["WechatJsToken"].Value;
            JsConfig.WechatJsDomain = appsection.Settings["WechatJsDomain"].Value;
            JsConfig.WechatJsReturnDomain = appsection.Settings["WechatJsReturnDomain"].Value;


            return JsConfig;
        }

        public static WebchatJsUserinfo GetUserInfo(string userAgent, string CODE)
        {
            WechatConfig wechatconfig = AccessTokenService.GetWechatConfig();
            WebchatJsUserinfo userinfo = new WebchatJsUserinfo();
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + wechatconfig.Appid + "&secret=" + wechatconfig.AppSecret + "&code=" + CODE + "&grant_type=authorization_code";
       
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);

            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            WechatJsToken token = JsonConvert.DeserializeObject<WechatJsToken>(result);


            string url2 = "https://api.weixin.qq.com/sns/userinfo?access_token=" + token.access_token + "&openid=" + token.openid + "&lang=zh_CN";

            HttpWebResponse res = HttpWebResponseUtility.CreateGetHttpResponse(url2, null, userAgent, null);
            Stream stream2 = res.GetResponseStream();
            StreamReader sr2 = new StreamReader(stream2);
            string result2 = sr2.ReadToEnd();

            userinfo = JsonConvert.DeserializeObject<WebchatJsUserinfo>(result2);

            return userinfo;
        
        }
      
    }
}