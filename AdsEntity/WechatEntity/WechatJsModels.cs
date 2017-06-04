using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdsEntity.WechatEntity
{
    /// <summary>
    /// 获取js的ticket
    /// </summary>
    public class WechatJsTicket
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public string expires_in { get; set; }
    }
    public class WechatJsConfig
    {
        public string WechatJsUrl { get; set; }
        public string WechatJsToken { get; set; }
        public string WechatJsDomain { get; set; }
        public string WechatJsReturnDomain { get; set; }
    }

    public class WechatJsToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }
    }

    public class WebchatJsUserinfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }

        public string province { get; set; }

        public string city { get; set; }
        public string country { get; set; }

        public string headimgurl { get; set; }
        public string[] privilege { get; set; }
        public string unionid { get; set; }




    }



}