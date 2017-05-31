using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdsWap.Controllers
{
    public class WechatController : Controller
    {
        //
        // GET: /Wechat/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Wechat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Wechat/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Wechat/Create
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
        // GET: /Wechat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Wechat/Edit/5
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
        // GET: /Wechat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Wechat/Delete/5
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
