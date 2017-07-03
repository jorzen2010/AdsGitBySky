using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using AdsEntity;
using Common;
namespace AdsWeb.WechatServices
{
    public class WechatMenuServices
    {
        public static string GetMenu(string access_token)
        {
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", access_token);

            HttpWebResponse res = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);
            Stream stream = res.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            return result;
           
        }

        public static string  CreateMenu(string access_token, string postdata)
        {
           string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", access_token);

           string result = HttpWebResponseUtility.PostJsonData(url, postdata);

           return result;
            
        }

        public static string DeleteMenu(string access_token)
        {
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", access_token);

            HttpWebResponse res = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);
            Stream stream = res.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            return result;

        }


    }
}