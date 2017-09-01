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
    public class WechatQRController : Controller
    {
        //
        // GET: /WechatQR/
        public ActionResult CreateTempQR()
        {
            WechatIDQRInfo IdQR = new WechatIDQRInfo();
            IdQR.expire_seconds = 604800;
            IdQR.action_name = "QR_SCENE";
            action_info action=new action_info();
            IdQR.action_info = action;   
            scene scene = new scene();
            scene.scene_id = 123456;

            string postData = JsonConvert.SerializeObject(IdQR);
            string token=AccessTokenService.GetAccessToken();
            string result = WechatQRServices.CreateTempQR(token, postData);

            ViewBag.result = result;
            return View("Result");
        }

        public ActionResult CreateForeverQR()
        {
            WechatIDQRInfo IdQR = new WechatIDQRInfo();
            IdQR.action_name = "QR_SCENE";
            action_info action = new action_info();
            IdQR.action_info = action;
            scene scene = new scene();
            scene.scene_str = "stringqr";

            string postData = JsonConvert.SerializeObject(IdQR);
            string token = AccessTokenService.GetAccessToken();
            string result = WechatQRServices.CreateForeverQR(token, postData);

            ViewBag.result = result;
            return View("Result");
        }

        public ActionResult GetQRByTicket()
        {
            string ticket = HttpUtility.UrlEncode("gQFs8TwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAydTZHOUYxdm5kYjIxd0ktT05wMW8AAgSsA6lZAwSAOgkA");
            string url = string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", ticket);
            return Redirect(url);
           
        }
	}
}