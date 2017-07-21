using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using AdsDal;
using AdsEntity;
using System.Data;
using System.Collections;
using Common;

namespace SkyWebApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private SkyWebContext db = new SkyWebContext();

        //
        // GET: /Account/
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            if (!string.IsNullOrEmpty(Request["returnUrl"]))
            {

                ViewBag.returnUrl = Request["returnUrl"].ToString();
                ViewBag.msg = "对不起，您尚未登陆不允许访问！";

            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies["uname"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["uid"].Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Session["uid"] = null;
            System.Web.HttpContext.Current.Session["uname"] = null;
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string username = fc["UserName"];
            string password = CommonTools.ToMd5(fc["Password"].ToString());
            bool rememberme = fc["RememberMe"] == null ? false : true;
            string reurnUrl = fc["ReturnUrl"];




            var sysUsers = from s in db.SysUsers
                           orderby s.SysUserId ascending
                           where (s.SysUserName == username && (s.SysPassword == password))
                           select s;
            if (sysUsers.Count() > 0)
            {
                if (sysUsers.First().SysStatus)
                {
                    HttpCookie cookie = new HttpCookie("uname");
                    cookie.Value = username;
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                    HttpCookie cookieid = new HttpCookie("uid");
                    cookieid.Value = sysUsers.First().SysUserId.ToString();
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookieid);

                    System.Web.HttpContext.Current.Session["uid"] = sysUsers.First().SysUserId.ToString();
                    System.Web.HttpContext.Current.Session["uname"] = username;

                    FormsAuthentication.SetAuthCookie(username, rememberme);

                    if (string.IsNullOrEmpty(reurnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        ViewBag.msg = "没有权限";
                        return Redirect(reurnUrl);
                    }

                }

                else
                {
                    ViewBag.msg = "此已经被禁用，不允许登陆";
                    return View();
                
                }
                
            }
            else
            {
                ViewBag.msg = "用户名或密码错误了";
                return View();

            }


        }
    }
}