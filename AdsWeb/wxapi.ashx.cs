using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Common;
using System.IO;
using AdsWeb.WechatServices;

namespace AdsWeb
{
    /// <summary>
    /// wxapi 的摘要说明
    /// </summary>
    public class wxapi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           
            if (System.Web.HttpContext.Current.Request.RequestType == "POST")
            {
               
                //*********************************自动应答代码块*********************************
                string postString = string.Empty;
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    //接收的消息为GBK格式
                    postString = Encoding.GetEncoding("GBK").GetString(postBytes);

                    WechatService.Excute(postString);

                }
                //********************************自动应答代码块end*******************************



            }
            else
            {
                //微信接入的测试
                WechatService.Auth();
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