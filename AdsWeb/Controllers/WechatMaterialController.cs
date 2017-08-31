using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AdsWeb.WechatServices;
using AdsEntity.WechatEntity;

namespace AdsWeb.Controllers
{
    public class WechatMaterialController : Controller
    {
        //
        // GET: /WechatMaterial/
        public ActionResult TempMaterialList()
        {
            return View();
        }

        public ActionResult AddMaterial()
        {
            return View();
        }

        public ActionResult UploadMaterial()
        {
            string type = Request.Form["MaterialType"];

            var file = Request.Files["MaterialFile"];
            string token = AccessTokenService.GetAccessToken();

            var media_id = WechatMaterialServices.UploadForeverMedia(token, type, file.FileName, file.InputStream);
            string filename = file.FileName;
            ViewBag.filename = filename;
            ViewBag.type = type;
            ViewBag.info = media_id;
            return View();
        }

        public ActionResult GetMaterialList()
        {
            MaterialListPost postobj = new MaterialListPost();
            postobj.count = 20;
            postobj.type = "image";
            postobj.offset = 0;
             string token = AccessTokenService.GetAccessToken();
            string postJsonStr = JsonConvert.SerializeObject(postobj);
            string result = WechatMaterialServices.GetMaterialList(token, postJsonStr);

            WechatResult wechatResult = JsonConvert.DeserializeObject<WechatResult>(result);
            if (wechatResult.errcode != 0)
            {
                ViewBag.msg = "获取素材列表失败！返回错误代码如下：";
                ViewBag.content = result;
            }
            else
            {
                MaterialList materialList = JsonConvert.DeserializeObject<MaterialList>(result);
                ViewData["materialList"] = materialList.item;

                ViewBag.msg = "success";
            }
            

            return View();

        }
	}
}