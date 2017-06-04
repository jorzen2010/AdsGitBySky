using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdsEntity.WechatEntity
{
    public class WechatAccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }

    public class WechatError
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
    public class WechatConfig
    {
        public string Appid { get; set; }
        public string AppSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenExpiredTime { get; set; }
    }

    
}