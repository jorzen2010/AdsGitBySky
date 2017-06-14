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
        public int BabyId { get; set; }
        public int CustomerId { get; set; }
         [Display(Name = "姓名")]
        public string BabyName { get; set; }
        [Display(Name = "头像")]
         public string BabyAvatar { get; set; }
        [Display(Name = "性别")]
         public string BabySex { get; set; }
        [Display(Name = "生日")]
        public DateTime? BabyBirthday { get; set; }
        [Display(Name = "注册时间")]
        public DateTime BabyRegTime { get; set; }
        [Display(Name = "到期时间")]
        public DateTime? BabyExpiredTime { get; set; }
        [Display(Name = "状态")]
        public bool Babystatus { get; set; }


        ////未来可延伸部分母亲的生产年龄，看护方式，等等，通过问卷获取此信息

        //[Display(Name = "生育类型")]
        //public string BbShenchan { get; set; }
        //[Display(Name = "居住环境")]
        //public string BbHuanjing { get; set; }
        //[Display(Name = "发现年龄")]
        //public string BbFaxianNianling { get; set; }
        //[Display(Name = "主要看护人")]
        //public string BbKanhu { get; set; }


    }
}
