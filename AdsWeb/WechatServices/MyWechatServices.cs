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
    public class MyWechatServices
    {

        //邀请卡功能

        public static void invitationCard(string token,string ToUser,string PreMsg,string cardMediaId,string LastMsg)
        {
            string result = string.Empty;

            result = WechatCustomerServices.SendCustomerServiceTextMessge(token, PreMsg, ToUser);

           WechatResult wechatResult = JsonConvert.DeserializeObject<WechatResult>(result);
           if (wechatResult.errcode == 0)
           {
               result = WechatCustomerServices.SendCustomerServiceImageMessge(token, cardMediaId, ToUser);
               WechatResult  wechatResult2 = JsonConvert.DeserializeObject<WechatResult>(result);
               if (wechatResult2.errcode == 0)
               {
                   WechatCustomerServices.SendCustomerServiceTextMessge(token, LastMsg, ToUser);
               }
             
           }  
        }

    }
}