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
    public class AdsBabyController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "AdsBaby";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "BabyId";
            pager.FiledOrder = "BabyId desc";

            pager = CommonDal.GetPager(pager);
            IList<AdsBaby> babys = DataConvertHelper<AdsBaby>.ConvertToModel(pager.EntityDataTable);
            var babysAsIPageList = new StaticPagedList<AdsBaby>(babys, pager.PageNo, pager.PageSize, pager.Amount);
            return View(babysAsIPageList);
        }

        // GET: /AdsBaby/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsBaby adsbaby = db.AdsBabys.Find(id);
            if (adsbaby == null)
            {
                return HttpNotFound();
            }
            return View(adsbaby);
        }

        // GET: /AdsBaby/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AdsBaby/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BabyId,CustomerId,BabyName,BabyAvatar,BabySex,BabyBirthday,BabyRegTime,BabyExpiredTime")] AdsBaby adsbaby)
        {
            if (ModelState.IsValid)
            {
                db.AdsBabys.Add(adsbaby);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adsbaby);
        }

        // GET: /AdsBaby/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsBaby adsbaby = db.AdsBabys.Find(id);
            if (adsbaby == null)
            {
                return HttpNotFound();
            }
            return View(adsbaby);
        }

        // POST: /AdsBaby/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BabyId,CustomerId,BabyName,BabyAvatar,BabySex,BabyBirthday,BabyRegTime,BabyExpiredTime")] AdsBaby adsbaby)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adsbaby).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adsbaby);
        }

        // GET: /AdsBaby/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsBaby adsbaby = db.AdsBabys.Find(id);
            if (adsbaby == null)
            {
                return HttpNotFound();
            }
            return View(adsbaby);
        }

        // POST: /AdsBaby/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdsBaby adsbaby = db.AdsBabys.Find(id);
            db.AdsBabys.Remove(adsbaby);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
