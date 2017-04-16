using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace AdsWeb.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/
        public JsonResult Upload(string rootpath, string folder)
        {
            Message msg = new Message();
            if (Request.Files.Count > 0 && Request.Files[0] != null && !string.IsNullOrEmpty(Request.Files[0].FileName))
            {
                try
                {
                    string fileName = FileServices.Uploadfiles(rootpath, folder, Request.Files[0]);
                    msg.MessageStatus = "true";
                    msg.MessageInfo = "上传成功";
                    msg.MessageUrl = fileName;

                }
                catch (Exception ex)
                {
                    msg.MessageStatus = "false";
                    msg.MessageInfo = "上传失败" + ex.Message;
                }
            }
            else
            {
                msg.MessageStatus = "false";
                msg.MessageInfo = "未选择文件";

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadImgBase64(string img,string id,string rootpath, string folder)
        {
            Message msg = new Message();
            Random random = new Random();
           
                try
                {
                    string filePath = FileServices.UploadBase64(rootpath, folder, img);                     
                    msg.MessageStatus = "true";
                    msg.MessageInfo = "上传成功";
                    msg.MessageUrl = filePath;

                }
                catch (Exception ex)
                {
                    msg.MessageStatus = "false";
                    msg.MessageInfo = "上传失败" + ex.Message;
                }
          
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
	}
}