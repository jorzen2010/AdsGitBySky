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
    public class ScaleController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /Scale/
        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Scale";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "ScaleId";
            pager.FiledOrder = "ScaleId desc";

            pager = CommonDal.GetPager(pager);
            IList<Scale> scales = DataConvertHelper<Scale>.ConvertToModel(pager.EntityDataTable);
            var scalesAsIPageList = new StaticPagedList<Scale>(scales, pager.PageNo, pager.PageSize, pager.Amount);
            return View(scalesAsIPageList);
        }


       
        // GET: /Scale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Scale/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ScaleId,ScaleName,ScaleInfo")] Scale scale)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.scaleRepository.Insert(scale);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(scale);
        }


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

                unitOfWork.scaleRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}
