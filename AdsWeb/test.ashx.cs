using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AdsWeb
{
    /// <summary>
    /// test 的摘要说明
    /// </summary>
    public class test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id=context.Request.QueryString["id"];
            string url = context.Request.QueryString["url"];
            asdferror err = new asdferror();
            string result = "";
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(url))
            {
                err.errorcode = "1000";
                err.erromsg = "invailed id or url";

                result = JsonConvert.SerializeObject(err);

            }
            else
            { 
            asdf t = new asdf();
            t.id = id;
            t.url = url;
            result = JsonConvert.SerializeObject(t);
            }
            

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class asdf
    {
        public string id { get; set; }
        public string url { get; set; }
    
    }
    public class asdferror
    {
        public string errorcode { get; set; }
        public string erromsg { get; set; }

    }
}