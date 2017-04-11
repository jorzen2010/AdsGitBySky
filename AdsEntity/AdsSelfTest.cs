using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsSelfTest
    {
        [Key]
        public int SelfTestId { get; set; }
        public int BabyId { get; set; }
        public int ParentId { get; set; }
        public int VideoId { get; set; }
        public int Test { get; set; }
    }
}
