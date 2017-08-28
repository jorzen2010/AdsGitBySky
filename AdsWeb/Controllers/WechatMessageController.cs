using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdsEntity.WechatEntity;
using Common;
using Newtonsoft.Json;
using AdsWeb.WechatServices;

namespace AdsWeb.Controllers
{
    public class WechatMessageController : Controller
    {
        //
        // GET: /WechatMessage/
        public ActionResult TempletMessage()
        {
            return View();

        }
        public ActionResult SendTempletMessage()
        {
            WechatTemplateMessage msgData = new WechatTemplateMessage
            {
                touser = "on5dLwiMZ_E06gwpY2pq3Bj4glZ8",
                template_id = "S0jkUpR2R7C6PIpnJHRD1GxIad27dln4vEOtD7uRl4A",
                url = "http://www.baidu.com",
                data = new
                {

                    first = new
                    {
                        value = "宝贝，你好，训练时间到了",
                        color = "#173177"
                    },
                    keyword1 = new
                    {
                        value = "踢皮球运动",
                        color = "#173177"
                    },
                    keyword2 = new
                    {
                        value = "30分钟",
                        color = "#173177"
                    },
                    remark = new
                    {
                        value = "感谢您的参与。"
                    }
                }
            };

            string access_token = AccessTokenService.GetAccessToken();
            string postdata = JsonConvert.SerializeObject(msgData);

            string result = WechatMessageServices.SendTempletMessge(access_token, postdata);

            WechatResult wechatResult = JsonConvert.DeserializeObject<WechatResult>(result);
            if (wechatResult.errcode == 0)
            {
                ViewBag.msg = "模板消息发送成功！操作代码如下：";
                ViewBag.result = result;
            }
            else
            {
                ViewBag.msg = "模板消息发送失败！错误代码如下：";
                ViewBag.result = result;

            }

            return View();
        }
	}
}