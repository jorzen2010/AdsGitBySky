using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdsWeb.AliyunVideo
{
    public class VideoInfo
    {
        public string RequestId { get; set; }
        public VideoMeta VideoMeta { get; set; }
        public string PlayAuth { get; set; }
        
    }
    public class VideoMeta
    {
        public string RequestId { get; set; }
        public string Status { get; set; }
        public string VideoId { get; set; }
        public string Duration { get; set; }
        public string Title { get; set; }

    
    }
}