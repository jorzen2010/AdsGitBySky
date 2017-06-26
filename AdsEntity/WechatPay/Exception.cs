using System;
using System.Collections.Generic;
using System.Web;

namespace AdsEntity.WechatPay
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}