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
        [Display(Name = "姓名")]
        public string BbName { get; set; }
        [Display(Name = "性别")]
        public string BbSex { get; set; }
        [Display(Name = "生日")]
        public DateTime BbBirthday { get; set; }

        [Display(Name = "用户名")]
        public string BbUserName { get; set; }
        [Display(Name = "密码")]
        public string BbPassword { get; set; }


        //未来可延伸部分母亲的生产年龄，看护方式，等等


        [Display(Name = "省份")]
        public string BbShengfen { get; set; }
        [Display(Name = "城市")]
        public string BbCity { get; set; }
        [Display(Name = "地区")]
        public string BbDiqu { get; set; }
        [Display(Name = "详细住址")]
        public string BbAddress { get; set; }

        [Display(Name = "生产类型")]
        public string BbShenchan { get; set; }
        [Display(Name = "居住环境")]
        public string BbHuanjing { get; set; }
        [Display(Name = "发现年龄")]
        public string BbFaxianNianling { get; set; }
        [Display(Name = "主要看护人")]
        public string BbKanhu { get; set; }


    }
}
