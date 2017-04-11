using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class SysUser
    {
        [Key]
        public int SysUserId { get; set; }
        [Display(Name = "用户名")]
        public string SysUserName { get; set; }
        [Display(Name = "密码")]
        public string SysPassword { get; set; }
        [Display(Name = "邮箱")]
        public string SysEmail { get; set; }
        [Display(Name = "是否启用")]
        public bool SysStatus { get; set; }
        [Display(Name = "是否删除")]
        public bool SysIfDel { get; set; }
    }
}
