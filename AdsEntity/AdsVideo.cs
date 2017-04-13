using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsVideo
    {
        [Key]
        public int VideoId { get; set; }
        [Display(Name="音视频名称")]
        public string VideoName { get; set; }
        [Display(Name = "音视频地址")]
        public string VideoUrl { get; set; }
        [Display(Name = "音视频编号")]
        public string VideoNumber { get; set; }
        [Display(Name = "音视频类别")]
        public int VideoCategory { get; set; }
    }
}
