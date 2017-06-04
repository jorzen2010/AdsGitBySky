using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdsEntity.WechatEntity
{
    /// <summary>
    /// 模板消息
    /// </summary>
    public class WechatTemplateMessage
    {
        public string touser{get;set;}
        public string template_id{get;set;}
        public string url{get;set;}
        public miniprogram  miniprogram{get;set;}
        public object data{get;set;}
    }

    /// <summary>
    /// 小程序
    /// </summary>
    public class miniprogram
    {
        public string appid{get;set;}
        public string pagepath{get;set;}
    
    }
    /// <summary>
    /// 批量获取用户
    /// </summary>
    public class UserList
    {
        public UserListItem[] user_list { get; set; }

    }
    /// <summary>
    /// 批量获取用户项目
    /// </summary>
    public class UserListItem
    {
        public string openid { get; set; }
        public string lang { get; set; }
    }

       
}