using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsEntity.WechatEntity
{

    public class CustomerServiceTextMessage
    {
        public string touser { get; set; }
        public string msgtype { get; set; }
        public text text { get; set; }
    
    }

    public class text 
    {
        public string content { get; set; }
    }
    public class CustomerServiceImageMessage
    {
        public string touser { get; set; }
        public string msgtype { get; set; }
        public image image { get; set; }

    }
    public class image
    {
        public string media_id { get; set; }
    }
}
