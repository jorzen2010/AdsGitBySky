using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsEntity.WechatEntity
{
    //获取素材列表的请求参数类
   public  class MaterialListPost
    {
       public string type { get; set; }
       public int offset { get; set; }
       public int count { get; set; }
    }

   //获取素材列表的请求参数类
   public class MaterialList
   {
       public int total_count { get; set; }
       public int item_count { get; set; }
       public List<MaterialItem> item { get; set; }
   }

   public class MaterialItem
   {
       public string media_id { get; set; }
       public string name { get; set; }
       public string update_time { get; set; }
       public string url { get; set; }
   
   }

}
