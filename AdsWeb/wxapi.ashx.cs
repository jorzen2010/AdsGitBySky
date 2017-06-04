using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace AdsWeb
{
    /// <summary>
    /// wxapi 的摘要说明
    /// </summary>
    public class wxapi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string token = "zhaozheng";
            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = SkyEncrypt.SHA1(tmpStr);

            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {
                System.Web.HttpContext.Current.Response.Write(echoString);
                System.Web.HttpContext.Current.Response.End();
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("验证不通过");
                System.Web.HttpContext.Current.Response.End();
            }


        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}