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

    public class SysUserController : BaseController
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
            SysUser sysuser = unitOfWork.sysUsersRepository.GetByID(id);
            sysuser.SysStatus = status;
            if (ModelState.IsValid)
            {
               
                unitOfWork.sysUsersRepository.Update(sysuser);
                unitOfWork.Save();
                msg.MessageStatus = "true";
                msg.MessageInfo = "已经更改为" + sysuser.SysStatus.ToString();
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ResetPassword(int id)
        {
            Message msg = new Message();

            SysUser sysuser = unitOfWork.sysUsersRepository.GetByID(id);
            string password = CommonTools.GenerateRandomNumber(8);
            string confirmpassword = CommonTools.ToMd5(password);
            sysuser.SysPassword = confirmpassword;

           
            

            if (ModelState.IsValid)
            {

                unitOfWork.sysUsersRepository.Update(sysuser);
                unitOfWork.Save();
                string EmailContent = "密码已经被重置为" + password.ToString() + "，并已经发送邮件到" + sysuser.SysEmail+",请注意查收！";
                AdsEmailServices.SendEmail(EmailContent,sysuser.SysEmail);
                msg.MessageStatus = "true";
                msg.MessageInfo = EmailContent;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        // POST: /SysUser/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SysUserId,SysUserName,SysPassword,SysEmail,SysStatus")] SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.sysUsersRepository.Insert(sysuser);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(sysuser);
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
               
                unitOfWork.sysUsersRepository.Delete(id);
                unitOfWork.Save();

                msg.MessageStatus = "true";
                msg.MessageInfo = "删除成功";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


    }


}
