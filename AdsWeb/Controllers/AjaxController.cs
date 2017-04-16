using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace AdsWeb.Controllers
{
    public class AjaxController : Controller
    {
        //
        //检查身份证号码
        public JsonResult VerifyIdCard(string idcard)
        {
            ValidObj validObj = new ValidObj();
            bool result = CommonTools.Verify(idcard);
            validObj.valid = result;
            return Json(validObj, JsonRequestBehavior.AllowGet);
        }
        //检查用户名重复
        public JsonResult CheckUserName(string uname)
        {
            ValidObj validObj = new ValidObj();
            bool result =true;
            validObj.valid = result;
            return Json(validObj, JsonRequestBehavior.AllowGet);
        }
	}







    //为bootstrap的validate框架写一个验证类别，格式为json格式。例如{'valid':true}
    public class ValidObj
    {
        public bool valid;
    }
}