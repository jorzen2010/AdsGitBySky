using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsCustomer
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "用户名")]
        public string CustomerUserName { get; set; }
        [Display(Name = "邮箱")]
        public string CustomerEmail { get; set; }
        [Display(Name = "密码")]
        public string CustomerPassword { get; set; }

        [Display(Name = "申请状态")]
        public IdentiyStatus CustomerIdentity { get; set; }

        [Display(Name = "openid")]
        public string CustomerOpenid { get; set; }
        [Display(Name = "unionid")]
        public string CustomerUnionid { get; set; }



        [Display(Name = "头像")]
        public string CustomerAvatar { get; set; }
        [Display(Name = "姓名")]
        public string CustomerNickName { get; set; }
        [Display(Name = "性别")]
        public string CustomerSex { get; set; }
        [Display(Name = "生日")]
        public DateTime? CustomerBirthday { get; set; }
        [Display(Name = "生日类型")]
        public string CustomerBirthdayType { get; set; }
        [Display(Name = "省份")]
        public string CustomerProvince { get; set; }
        [Display(Name = "城市")]
        public string CustomerCity { get; set; }
        [Display(Name = "地区")]
        public string CustomerDistrict { get; set; }
        [Display(Name = "详细住址")]
        public string CustomerAddress { get; set; }
        [Display(Name = "注册时间")]
        public DateTime CustomerRegTime { get; set; }
        [Display(Name = "最后登录时间")]
        public DateTime CustomerLastLoginTime { get; set; }




        //[Display(Name = "客户类型")]
        //public int CustomerRole { get; set; }
        //[Display(Name = "状态")]
        //public bool CustomerStatus { get; set; }



        ////身份认证

        //[Display(Name = "姓名")]
        //public string CustomerRealName { get; set; }

        //[Display(Name = "身份证号")]
        //public string CustomerIDCard { get; set; }
        //[Display(Name = "手持身份证")]
        //public string CustomerHoldCard { get; set; }
        //[Display(Name = "身份证号正面")]
        //public string CustomerIDCardzm { get; set; }
        //[Display(Name = "身份证号反面")]
        //public string CustomerIDCardsm { get; set; }

        ////第三方信息认证
        //[Display(Name = "手机号码")]
        //public string CustomerTelephone { get; set; }
        //[Display(Name = "QQ号码")]
        //public string CustomerQQ { get; set; }
        //[Display(Name = "微信号码")]
        //public string CustomerWechat { get; set; }

        public enum IdentiyStatus
        { 
            未申请计划=1,
            已申请计划=2,
            申请未付款=3
        }


    }
}
