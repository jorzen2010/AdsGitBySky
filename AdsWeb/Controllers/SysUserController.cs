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

namespace AdsWeb.Controllers
{
    public class SysUserController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /SysUser/
        public ActionResult Index()
        {
            
            return View(unitOfWork.AdsSysUsersRepository.Get());
        }

        // GET: /SysUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsSysUser adssysuser = unitOfWork.AdsSysUsersRepository.GetByID(id);
            if (adssysuser == null)
            {
                return HttpNotFound();
            }
            return View(adssysuser);
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
        public ActionResult Create([Bind(Include="AdsSysUserId,AdsSysUserName,AdsSysPassword")] AdsSysUser adssysuser)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.AdsSysUsersRepository.Insert(adssysuser);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(adssysuser);
        }

        // GET: /SysUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsSysUser adssysuser = unitOfWork.AdsSysUsersRepository.GetByID(id);
            if (adssysuser == null)
            {
                return HttpNotFound();
            }
            return View(adssysuser);
        }

        // POST: /SysUser/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AdsSysUserId,AdsSysUserName,AdsSysPassword")] AdsSysUser adssysuser)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.AdsSysUsersRepository.Update(adssysuser);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(adssysuser);
        }

        // GET: /SysUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsSysUser adssysuser = unitOfWork.AdsSysUsersRepository.GetByID(id);
            if (adssysuser == null)
            {
                return HttpNotFound();
            }
            return View(adssysuser);
        }

        // POST: /SysUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdsSysUser adssysuser = unitOfWork.AdsSysUsersRepository.GetByID(id);
            unitOfWork.AdsSysUsersRepository.Delete(adssysuser);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


    }
}
