using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsOrganization
    {
        [Key]
        public int OrgId { get; set; }
        [Display(Name = "机构名称")]
        public string OrgName { get; set; }
         [Display(Name = "机构简介")]
        public string OrgDescription { get; set; }
        [Display(Name = "省份")]
        public string OrgShengfen { get; set; }
        [Display(Name = "城市")]
        public string OrgCity { get; set; }
        [Display(Name = "地区")]
        public string OrgDiqu { get; set; }
        [Display(Name = "地址")]
        public string OrgAddress { get; set; }
        [Display(Name = "联系人")]
        public string OrgContactName { get; set; }
        [Display(Name = "联系电话")]
        public string OrgContactTel { get; set; }
    }
}
