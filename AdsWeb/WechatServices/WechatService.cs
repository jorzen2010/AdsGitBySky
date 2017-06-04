using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using AdsEntity.WechatEntity;
using Newtonsoft.Json;

namespace AdsWeb.WechatServices
{
    public class WechatService
    {
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


                    
            }
            result = HttpWebResponseUtility.PostJsonData(url, postdata);
            return result;
        }
    }
}