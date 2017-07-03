using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdsEntity;
using Common;
namespace AdsWeb.WechatServices
{
    public class WechatMenuServices
    {
        public static string GetMenu(string access_token)
        {
           
        }

        public static string  CreateMenu(string access_token, string postdata)
        {
           string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", access_token);

           string result = HttpWebResponseUtility.PostJsonData(url, postdata);

           return result;
            
        }

        public static void DeleteMenu()
        {

        }


    }
}