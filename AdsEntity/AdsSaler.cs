using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsSaler
    {
        [Key]
        public int SalerId { get; set; }
        [Display(Name = "销售员编码")]
        public int SalerCode { get; set; }
        [Display(Name = "销售员姓名")]
        public string SalerName { get; set; }

    }
}
