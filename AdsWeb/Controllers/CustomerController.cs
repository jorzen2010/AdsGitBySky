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
using PagedList;
using Common;
using PagedList.Mvc;

namespace AdsWeb.Controllers
{
    public class CustomerController : Controller
    {
        private SkyWebContext db = new SkyWebContext();
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: /SysUser/Default1
        public ActionResult Index(int? page)
        {
            Pager pager = new Pager();
            pager.table = "AdsCustomer";
            pager.strwhere = "1=1";
            pager.PageSize = 2;
            pager.PageNo = page ?? 1;
            pager.FieldKey = "CustomerId";
            pager.FiledOrder = "CustomerId desc";


            IList<AdsCustomer> customers = CustomerServices.GetListForPageList(pager);
            var customersAsIPageList = new StaticPagedList<AdsCustomer>(customers, pager.PageNo, pager.PageSize, pager.Amount);
            return View(customersAsIPageList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ResetPassword(int id)
        {
            Message msg = new Message();

            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
            string password = CommonTools.GenerateRandomNumber(8);
            string confirmpassword = CommonTools.ToMd5(password);
            customer.CustomerPassword = confirmpassword;
            if (ModelState.IsValid)
            {

                unitOfWork.adsCustomersRepository.Update(customer);
                unitOfWork.Save();
                string EmailContent = "密码已经被重置为" + password.ToString() + "，并已经发送邮件到" + customer.CustomerEmail + ",请注意查收！";
                AdsEmailServices.SendEmail(EmailContent, customer.CustomerEmail);
                msg.MessageStatus = "true";
                msg.MessageInfo = EmailContent;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(int? id, bool status)
        {
            Message msg = new Message();
            if (id == null)
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "找不到ID";
            }
            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
            customer.CustomerStatus = status;
            if (ModelState.IsValid)
            {
                unitOfWork.adsCustomersRepository.Update(customer);
                unitOfWork.Save();
                msg.MessageStatus = "true";
                msg.MessageInfo = "已经更改为" + customer.CustomerStatus.ToString();
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsCustomer adscustomer = db.AdsCustomers.Find(id);
            if (adscustomer == null)
            {
                return HttpNotFound();
            }
            return View(adscustomer);
        }

        // GET: /Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdsCustomer adscustomer)
        {
            if (ModelState.IsValid)
            {
                db.AdsCustomers.Add(adscustomer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adscustomer);
        }

        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsCustomer adscustomer = db.AdsCustomers.Find(id);
            if (adscustomer == null)
            {
                return HttpNotFound();
            }
            return View(adscustomer);
        }

        public ActionResult IdentityEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsCustomer adscustomer = db.AdsCustomers.Find(id);
            if (adscustomer == null)
            {
                return HttpNotFound();
            }
            return View(adscustomer);
        }

        // POST: /Customer/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection fc)
        {
            int id = int.Parse(fc["CustomerId"]);
            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);

            customer.CustomerRealName = fc["CustomerRealName"];
            customer.CustomerBirthdayType = fc["CustomerBirthdayType"];
            customer.CustomerBirthday = DateTime.Parse(fc["CustomerBirthday"]);
            customer.CustomerSex = fc["CustomerSex"];
            customer.CustomerProvince = fc["CustomerProvince"];
            customer.CustomerCity = fc["CustomerCity"];
            customer.CustomerDistrict = fc["CustomerDistrict"];
            customer.CustomerAddress = fc["CustomerAddress"];

            if (ModelState.IsValid)
            {

                unitOfWork.adsCustomersRepository.Update(customer);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(customer);
         
        }

        // GET: /Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsCustomer adscustomer = db.AdsCustomers.Find(id);
            if (adscustomer == null)
            {
                return HttpNotFound();
            }
            return View(adscustomer);
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

                unitOfWork.adsCustomersRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
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
