using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdsWeb.Controllers
{
    public class WxController : Controller
    {
        //
        // GET: /Wx/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Wx/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Wx/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Wx/Create
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
        // GET: /Wx/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Wx/Edit/5
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
        // GET: /Wx/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Wx/Delete/5
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
