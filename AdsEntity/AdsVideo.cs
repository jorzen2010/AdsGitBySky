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
        [Display(Name = "音视频编号")]
        public string VideoNumber { get; set; }
        [Display(Name="音视频名称")]
        public string VideoName { get; set; }
        [Display(Name = "音视频讲师")]
        public string VideoTeacher { get; set; }
        [Display(Name = "音视频封面")]
        public string VideoPhoto { get; set; }
        [Display(Name = "音视频地址（阿里云视频Id）")]
        public string VideoUrl { get; set; }
        [Display(Name = "音视频介绍")]
        public string VideoInfo { get; set; }
        [Display(Name = "音视频适用范围")]
        public string VideoFor { get; set; }
        [Display(Name = "音视频备注")]
        public string VideoBeizhu { get; set; }
       
        [Display(Name = "音视频类别")]
        public int VideoCategory { get; set; }
        [Display(Name = "训练权重")]
        public float VideoWeight { get; set; }
        [Display(Name = "音视频价格")]
        public float VideoPrice { get; set; }
        [Display(Name = "音视频会员价格")]
        public float VideoVIPPrice { get; set; }
        [Display(Name = "音视频折扣价格")]
        public float VideoZKPrice { get; set; }
        [Display(Name = "音视频设置免费")]
        public bool VideoFree { get; set; }
        [Display(Name = "音视频设置试看")]
        public bool VideoTry { get; set; }
        [Display(Name = "浏览次数")]
        public int VideoViewTimes { get; set; }
        [Display(Name = "音视频上线时间")]
        public DateTime VideoTime { get; set; }
    }
}
