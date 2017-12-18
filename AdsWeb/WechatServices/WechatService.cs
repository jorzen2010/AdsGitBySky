using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using Common;
using AdsEntity.WechatEntity;
using Newtonsoft.Json;
using System.Configuration;

namespace AdsWeb.WechatServices
{
    public class WechatService
    {
        public static void Auth()
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appsection = config.GetSection("appSettings") as AppSettingsSection;

            string token = appsection.Settings["WechatToken"].Value.ToString();
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

        //处理微信信息
        public static void  Excute(string postStr)
        {

            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appsection = config.GetSection("appSettings") as AppSettingsSection;

            string WechatId = appsection.Settings["WechatId"].Value.ToString();

            LogHelper.Info("构建xml");

            XmlDocument xmldoc = new XmlDocument();
            LogHelper.Info("加载字符串");
            xmldoc.Load(new System.IO.MemoryStream(System.Text.Encoding.GetEncoding("GB2312").GetBytes(postStr)));
            LogHelper.Info("查找字节");
            XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");
            XmlNode FromUserName=xmldoc.SelectSingleNode("/xml/FromUserName");
            LogHelper.Info(MsgType.InnerText);

            if (MsgType != null)
            {
                if (MsgType.InnerText == "event")
                {
                    XmlNode EventContent = xmldoc.SelectSingleNode("/xml/Event");
                    switch (EventContent.InnerText)
                    {
                        case "subscribe":
                            WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "您终于来了，感谢您的关注!\n\n 您是自闭症孩子家长吗？请您点击下方菜单了解本系统。\n\n 如有疑问，您可以拨打13945016428联系瑞夕老师进行咨询。");
                            break;
                        case "unsubscribe":
                            WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "我哪里做的不好了，你居然敢离开我。");
                            LogHelper.Info("我哪里做的不好了，你居然敢离开我。");
                            break;
                        case "CLICK":

                           string token = AccessTokenService.GetAccessToken();
                           string PreMsg= "感谢您的认可,正在为您生成专属邀请卡。";
                           string cardMediaId="slv-URo_tcvYdETXRXCkYC9LsbMh2atzB72d0NznT0o";
                           string LastMsg="你可以把这张图片发送给需要的人，让更多星星的孩子能够健康成长。";
                           MyWechatServices.invitationCard(token, FromUserName.InnerText, PreMsg, cardMediaId, LastMsg);
                            break;
                        case "LOCATION":
                           WechatMessageServices.ResponseSuccessMessage(FromUserName.InnerText, WechatId);
                            break;
                        default:
                            //这是非常好用的一个地方，打开公众号，我就会和你打招呼。
                           WechatMessageServices.ResponseSuccessMessage(FromUserName.InnerText, WechatId);
                           break;
                    }                
                }
                else
                {
                    switch (MsgType.InnerText)
                    {
                        case "image":
                            WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "好好玩的图片啊，你想告诉我什么呢？");
                            break;
                        case "text":

                              XmlNode TextContent = xmldoc.SelectSingleNode("/xml/Content");
                            string keywords = TextContent.InnerText;
                            if (string.IsNullOrEmpty(keywords))
                            {
                                WechatMessageServices.ResponseSuccessMessage(FromUserName.InnerText, WechatId);
                                WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "您好，这是自闭症儿童家庭训练和评估系统，请点击下方菜单开始使用。\n\n 如有疑问，您可以拨打13945016428联系瑞夕老师进行咨询。");
                           
                            }
                            else
                            {
                                switch (keywords)
                                {
                                    case "自闭症资料":
                                        WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "您好，请先分享图片到朋友圈，帮助更多的自闭症家庭。\n\n然后请使用百度网盘下载该资料。\n\n 链接地址：https://pan.baidu.com/s/1pLdwA3D \n 密码：zeju \n\n 同时建议您点击下方开始训练按钮，加入自闭症家庭训练计划，让孩子提升更快。");
                           
                                        break;
                                    default:
                                        WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "您好，这是自闭症儿童家庭训练和评估系统，请点击下方菜单开始使用。\n\n 如有疑问，您可以拨打13945016428联系瑞夕老师进行咨询。");
                           
                                        break;
                                }

                            }

                            break;

                        default:
                            WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "祝星星宝贝健康快乐成长。");
                            break;
                        //这几个一般用不到，我就没做判断。
                        //case "voice":
                        //    WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "这是对语音的回复");
                        //    break;
                        //case "video":
                        //    WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "这是对视频的回复");
                        //    break;
                        //case "shortvideo":
                        //    WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "这是对小视频的回复");
                        //    break;
                        //case "location":
                        //    WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "这是对地理位置的回复");
                        //    break;
                        //case "link":
                        //    WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "这是对链接的回复");
                        //    break;
                    }
                }


            }
            else
            {

                WechatMessageServices.ResponseTextMessage(FromUserName.InnerText, WechatId, "success");
                
            }

           


        }



        public static string wechatApi(string operate, string access_token, string postdata)
        {
            string result = "";
            string url = "";
            switch (operate)
            {
                case "SendTemplateMessage":
                url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", access_token);               
                break;

                case "GetUserListInfo":
                url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}", access_token);              
                break;

                case "CreateMenu":
                url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", access_token);
                break;


                    
            }
            result = HttpWebResponseUtility.PostJsonData(url, postdata);
            return result;
        }
    }
}