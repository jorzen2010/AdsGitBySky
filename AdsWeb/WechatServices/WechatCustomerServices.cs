using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using Common;
using AdsEntity.WechatEntity;

namespace AdsWeb.WechatServices
{
    public class WechatCustomerServices
    {
        public static string SendCustomerServiceTextMessge(string access_token, string content,string touser)
        {
            CustomerServiceTextMessage customerTextMsg = new CustomerServiceTextMessage();
            text textcontent = new text();
            textcontent.content = content;

            customerTextMsg.touser = touser;
            customerTextMsg.msgtype = "text";
            customerTextMsg.text = textcontent;

            string customerTextMsgStr = JsonConvert.SerializeObject(customerTextMsg);



            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token);

            string result = HttpWebResponseUtility.PostJsonData(url, customerTextMsgStr);

            return result;

        }

        public static string SendCustomerServiceImageMessge(string access_token, string media_id, string touser)
        {
            CustomerServiceImageMessage customerImageMsg = new CustomerServiceImageMessage();
            image imageid = new image();
            imageid.media_id = media_id;

            customerImageMsg.touser = touser;
            customerImageMsg.msgtype = "image";
            customerImageMsg.image = imageid;

            string customerImageMsgStr = JsonConvert.SerializeObject(customerImageMsg);



            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token);

            string result = HttpWebResponseUtility.PostJsonData(url, customerImageMsgStr);

            return result;

        }
    }
}