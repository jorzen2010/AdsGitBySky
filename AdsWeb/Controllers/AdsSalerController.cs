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
    public class AdsSalerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "AdsSaler";
            pager.strwhere = "1=1";
            pager.PageSize = 20;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "SalerId";
            pager.FiledOrder = "SalerId desc";

            pager = CommonDal.GetPager(pager);
            IList<AdsSaler> salers = DataConvertHelper<AdsSaler>.ConvertToModel(pager.EntityDataTable);
            var salersAsIPageList = new StaticPagedList<AdsSaler>(salers, pager.PageNo, pager.PageSize, pager.Amount);
            return View(salersAsIPageList);
        }

        public ActionResult SaleBabys(int? page,int salercode,string salerName)
        {
            Pager pager = new Pager();
            pager.table = "AdsBaby";
            pager.strwhere = "SalerCode=" + salercode;
            pager.PageSize = 20;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "BabyId";
            pager.FiledOrder = "BabyId desc";

            pager = CommonDal.GetPager(pager);
            IList<AdsBaby> babys = DataConvertHelper<AdsBaby>.ConvertToModel(pager.EntityDataTable);
            var babysAsIPageList = new StaticPagedList<AdsBaby>(babys, pager.PageNo, pager.PageSize, pager.Amount);
            ViewBag.salerName = salerName;
            ViewBag.salercode = salercode;
            return View(babysAsIPageList);
        }

        // GET: /AdsSaler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsSaler adssaler = unitOfWork.adsSalerRepository.GetByID(id);
            if (adssaler == null)
            {
                return HttpNotFound();
            }
            return View(adssaler);
        }

        // GET: /AdsSaler/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AdsSaler/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SalerId,SalerCode,SalerName")] AdsSaler adssaler)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.adsSalerRepository.Insert(adssaler);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(adssaler);
        }

       
        // POST: /AdsSaler/Delete/5
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

                unitOfWork.adsSalerRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

       
    }
}
