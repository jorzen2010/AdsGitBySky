using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdsEntity;
using AdsDal;
using AdsServices;
using Common;
using PagedList;
using PagedList.Mvc;
namespace AdsWeb.Controllers
{
    public class VideoController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /Video/
        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "AdsVideo";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "VideoId";
            pager.FiledOrder = "VideoId desc";


            IList<AdsVideo> videoList = VideoServices.GetListForPageList(pager);
            var videoListAsIPageList = new StaticPagedList<AdsVideo>(videoList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(videoListAsIPageList);
        }

      

        public ActionResult Create()
        {
            CategoryServices categoryServices= new CategoryServices();
            ViewData["Categorylist"] = categoryServices.GetCategorySelectList(9);
            return View();
        }

        // POST: /Video/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="VideoId,VideoName,VideoUrl,VideoNumber,VideoCategory")] AdsVideo adsvideo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.adsVideosRepository.Insert(adsvideo);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(adsvideo);
        }

        // GET: /Video/Edit/5
        public ActionResult Edit(int? id)
        {
            CategoryServices categoryServices = new CategoryServices();
            ViewData["Categorylist"] = categoryServices.GetCategorySelectList(9);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsVideo adsvideo = unitOfWork.adsVideosRepository.GetByID(id);
            if (adsvideo == null)
            {
                return HttpNotFound();
            }
            return View(adsvideo);
        }

        // POST: /Video/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="VideoId,VideoName,VideoUrl,VideoNumber,VideoCategory")] AdsVideo adsvideo)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.adsVideosRepository.Update(adsvideo);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(adsvideo);
        }

        // POST: /Video/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteConfirmed(int? id)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            else
            {

                unitOfWork.adsVideosRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}
