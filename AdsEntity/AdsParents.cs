using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    
    public class AdsParents
    {
        [Key]
        public int ParentId { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "学历")]
        public string ParentXueli { get; set; }
        [Display(Name = "婚姻状态")]
        public string ParentHunyin { get; set; }
        [Display(Name = "职业")]
        public string ParentJob { get; set; }

    }
}
