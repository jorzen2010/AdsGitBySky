using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Common;

namespace AdsWeb.WechatServices
{
    public class WechatQRServices
    {
        public static string CreateTempQR(string access_token, string postdata)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", access_token);

            string result = HttpWebResponseUtility.PostJsonData(url, postdata);

            return result;

        }

        public static string CreateForeverQR(string access_token, string postdata)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", access_token);

            string result = HttpWebResponseUtility.PostJsonData(url, postdata);

            return result;

        }

        public static string GetQR(string ticket)
        {
            string url = string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={1}", ticket);
            return url;

        }
    }
}