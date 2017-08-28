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

       
        //public static string ResponsesubscribeMessage()
        //{
        //    string content = "订阅回复内容";
        //    string toUserName = "on5dLwiMZ_E06gwpY2pq3Bj4glZ8";
        //    return WechatMessageServices.sendMsgToManage(toUserName, content);
        //}

        //public static string ResponseMessage(string MsgType, string FromUserName)
        //{
        //    string toUserName = FromUserName;
        //     string content = string.Empty;

        //     switch (MsgType)
        //     {
        //         case "image":
        //             content = "图片回复";
        //             break;
        //         case "text":
        //             content = "文本回复";
        //             break;
        //         case "voice":
        //             content = "语音回复"; 
        //             break;
        //         case "video":
        //             content = "视频回复"; 
        //             break;
        //         case "shortvideo":
        //             content = "小视频回复"; 
        //             break;
        //         case "location":
        //             content = "地理位置回复"; 
        //             break;
        //         case "link":
        //             content = "链接回复"; 
        //             break;
        //     }
            
           
           
        //    return   WechatMessageServices.sendMsgToManage(toUserName, content);



        //}

        //public static string ResponseEventMessage(string MsgType, string FromUserName)
        //{
        //    string toUserName = FromUserName;
        //    string content = string.Empty;

        //    switch (MsgType)
        //    {
        //        case "image":
        //            content = "图片回复";
        //            break;
        //        case "text":
        //            content = "文本回复";
        //            break;
        //        case "voice":
        //            content = "语音回复"; break;
        //        case "video":
        //            content = "视频回复"; break;
        //        case "shortvideo":
        //            content = "小视频回复"; break;
        //        case "location":
        //            content = "地理位置回复"; break;
        //        case "link":
        //            content = "链接回复"; break;


        //    }



        //    return WechatMessageServices.sendMsgToManage(toUserName, content);



        //}
        //public static string ResponseImageMessage()
        //{

        //    string content = "图片测试回复";
        //    string toUserName = "on5dLwiMZ_E06gwpY2pq3Bj4glZ8";
        //    return WechatMessageServices.sendMsgToManage(toUserName, content);




        //}


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



        //private static string sendMsgToManage(string toUserName, string content)
        //{
        //    string managerweixinid = "gh_aca32f5ce076";
        //    string xmlMsg = "<xml>" +
        //    "<ToUserName><![CDATA[" + toUserName+ "]]></ToUserName>" +
        //    "<FromUserName><![CDATA[" + managerweixinid + "]]></FromUserName>" +
        //    "<CreateTime>12345678</CreateTime>" +
        //    "<MsgType><![CDATA[text]]></MsgType>" +
        //    "<Content><![CDATA[" + content + "]]></Content>" +
        //    "</xml>";

        //    return xmlMsg;

        //}




    }
}