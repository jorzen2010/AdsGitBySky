using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using AdsEntity;
using AdsDal;

namespace AdsWeb.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private SkyWebContext db = new SkyWebContext();
        // 只需重载此方法，模拟自定义的角色授权机制  
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
       
            base.OnAuthorization(filterContext);
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (httpContext == null)
            {
                throw new ArgumentException("HttpContext");
            }

           

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            else
            {
                return true;
            
            }
        }

    }
}