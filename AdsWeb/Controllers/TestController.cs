using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdsWeb.AliyunVideo;
using Common;
using System.Globalization;

namespace AdsWeb.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            string ApiUrl = AliyunCommonParaConfig.ApiUrl;
           // 注意这里需要使用UTC时间，比北京时间少8小时。
            string Timestamp = AliyunVideoServices.UrlEncode(DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo));
            string Action = "GetVideoPlayAuth";
            string SignatureNonce = CommonTools.EncryptToSHA1(CommonTools.GenerateRandomNumber(8));


            string VideoId = "61823886c6614369a10025d6fd4fff07";
            string StringToSign = AliyunVideoServices.GetStringToSign(VideoId, Timestamp, Action, SignatureNonce);
            string key=AliyunCommonParaConfig.AccessKeySecret+"&";
            string Signature = AliyunVideoServices.GetSignature(key, StringToSign);
            string SignUrl ="http://"+ApiUrl+"?"+ AliyunVideoServices.GetSignUrl(key, VideoId, Timestamp, Action, SignatureNonce);
            ViewBag.x = StringToSign;
            ViewBag.y = Signature;
            ViewBag.z = SignUrl;
            return View();
        }

        //
        // GET: /Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Test/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Test/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
