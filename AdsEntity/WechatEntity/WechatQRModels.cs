using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsEntity.WechatEntity
{
     public  class WechatIDQRInfo
    {
         public int expire_seconds { get; set; }
         public string action_name { get; set; }
         public action_info action_info { get; set; }

    }

     public class action_info
     {
         public scene scene{ get; set; }

     }

     public class scene
     {
         public string scene_str { get; set; }
         public int scene_id { get; set; }
     }

     public class WechatQRResult
     {
         public string ticket { get; set; }
         public string expire_seconds { get; set; }
         public int url { get; set; }
     
     
     }
}
