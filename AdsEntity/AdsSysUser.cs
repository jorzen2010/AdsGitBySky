using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsSysUser
    {
        public int AdsSysUserId { get; set; }
        [Display(Name = "用户名")]
        public string AdsSysUserName { get; set; }
        [Display(Name = "密码")]
        public string AdsSysPassword { get; set; }
    }
}
