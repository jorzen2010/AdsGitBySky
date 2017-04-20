using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsBaby
    {
        [Key]
        public int BbId { get; set; }
        public int CustomerId { get; set; }
        public string BbTags { get; set; }



        //未来可延伸部分母亲的生产年龄，看护方式，等等，通过问卷获取此信息

        [Display(Name = "生育类型")]
        public string BbShenchan { get; set; }
        [Display(Name = "居住环境")]
        public string BbHuanjing { get; set; }
        [Display(Name = "发现年龄")]
        public string BbFaxianNianling { get; set; }
        [Display(Name = "主要看护人")]
        public string BbKanhu { get; set; }


    }
}
