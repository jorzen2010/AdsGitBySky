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
    public class PingjiaController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /Video/
        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "Pingjia";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "PingjiaId";
            pager.FiledOrder = "PingjiaId desc";
            pager = CommonDal.GetPager(pager);
            IList<Pingjia> pingjiaList = DataConvertHelper<Pingjia>.ConvertToModel(pager.EntityDataTable);
            var pingjiaListAsIPageList = new StaticPagedList<Pingjia>(pingjiaList, pager.PageNo, pager.PageSize, pager.Amount);
            return View(pingjiaListAsIPageList);
        }


       
    }
}
