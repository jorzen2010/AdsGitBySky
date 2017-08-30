using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Common;


namespace AdsWeb.WechatServices
{
    public class WechatMessageServices
    {
        public static string SendTempletMessge(string access_token, string postdata)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", access_token);

            string result = HttpWebResponseUtility.PostJsonData(url, postdata);

            return result;

        }

       
      

        public static void ResponseTextMessage(string ToUserName,string FromUserName,string Content)
        {
            string CreateTime = TimeHelp.GetTimeStamp(System.DateTime.Now);

            string TextMsgXmlStr = "<xml>" +
            "<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>" +
            "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
            "<CreateTime>" + CreateTime + "</CreateTime>" +
            "<MsgType><![CDATA[text]]></MsgType>" +
            "<Content><![CDATA[" + Content + "]]></Content>" +
            "</xml>";

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Write(TextMsgXmlStr);

           // return TextMsgXmlStr;
        
        }

        public static void ResponseImageMessage(string ToUserName, string FromUserName, string media_id)
        {
            string CreateTime = TimeHelp.GetTimeStamp(System.DateTime.Now);

            string ImageMsgXmlStr = "<xml>" +
            "<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>" +
            "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
            "<CreateTime>" + CreateTime + "</CreateTime>" +
            "<MsgType><![CDATA[image]]></MsgType>" +
            "<Image>" +
            "<MediaId><![CDATA[" + media_id + "]]></MediaId>" +
            "</Image>"+
            "</xml>";

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Write(ImageMsgXmlStr);

        }

        public static void ResponseVoiceMessage(string ToUserName, string FromUserName, string media_id)
        {
            string CreateTime = TimeHelp.GetTimeStamp(System.DateTime.Now);

            string VoiceMsgXmlStr = "<xml>" +
            "<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>" +
            "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
            "<CreateTime>" + CreateTime + "</CreateTime>" +
            "<MsgType><![CDATA[voice]]></MsgType>" +
            "<Voice>" +
            "<MediaId><![CDATA[" + media_id + "]]></MediaId>" +
            "</Voice>" +
            "</xml>";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.Write(VoiceMsgXmlStr);
           

        }





    }
}