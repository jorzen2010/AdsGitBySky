using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdsWeb.AliyunVideo
{
    public class CommonParam
    {
        public string AccessKeyId { get; set; }
        public string Format { get; set; }            
        public string SignatureMethod { get; set; }
        public string SignatureNonce { get; set; }      
        public string SignatureVersion { get; set; }

        public string Timestamp { get; set; }
        public string Version { get; set; }
        public string Signature { get; set; }


    }
}