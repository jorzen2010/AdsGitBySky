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

    public class SysUserController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /SysUser/Default1
        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "SysUser";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "SysUserId";
            pager.FiledOrder = "SysUserId desc";


            IList<SysUser> sysUsers = SysServices.GetListForPageList(pager);
            var sysUsersAsIPageList = new StaticPagedList<SysUser>(sysUsers, pager.PageNo, pager.PageSize, pager.Amount);
            return View(sysUsersAsIPageList);
        }

        //public ActionResult index(int? page)
        //{
        //    return View(unitOfWork.sysUsersRepository.Get());
        //}

        // GET: /SysUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysuser = unitOfWork.sysUsersRepository.GetByID(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        // GET: /SysUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SysUser/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdsSysUserId,AdsSysUserName,AdsSysPassword")] SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.sysUsersRepository.Insert(sysuser);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(sysuser);
        }

        // GET: /SysUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysuser = unitOfWork.sysUsersRepository.GetByID(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        // POST: /SysUser/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SysUserId,SysUserName,SysPassword")] SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.sysUsersRepository.Update(sysuser);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(sysuser);
        }

        // GET: /SysUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysuser = unitOfWork.sysUsersRepository.GetByID(id);
            if (sysuser == null)
            {
                return HttpNotFound();
            }
            return View(sysuser);
        }

        // POST: /SysUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysuser = unitOfWork.sysUsersRepository.GetByID(id);
            unitOfWork.sysUsersRepository.Delete(sysuser);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


    }


}
