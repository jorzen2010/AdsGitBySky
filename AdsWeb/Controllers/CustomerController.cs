﻿using System;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdsCustomer customer)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.adsCustomersRepository.Insert(customer);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



        // GET: /Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        public ActionResult IdentityEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
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

            customer.CustomerNickName = fc["CustomerNickName"];
            customer.CustomerAvatar = fc["CustomerAvatar"];
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IdentityEdit(FormCollection fc)
        {
            int id = int.Parse(fc["CustomerId"]);
            AdsCustomer customer = unitOfWork.adsCustomersRepository.GetByID(id);

            customer.CustomerRealName = fc["CustomerRealName"];
            customer.CustomerIDCard = fc["CustomerIDCard"];
            customer.CustomerIDCardzm = fc["CustomerIDCardzm"];
            customer.CustomerIDCardsm = fc["CustomerIDCardsm"];
            customer.CustomerHoldCard = fc["CustomerHoldCard"];
            customer.CustomerIdentity = AdsCustomer.IdentiyStatus.正在审核;

            if (ModelState.IsValid)
            {

                unitOfWork.adsCustomersRepository.Update(customer);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(customer);

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

      
    }
}
