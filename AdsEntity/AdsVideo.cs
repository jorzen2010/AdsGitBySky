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
        public string VideoName { get; set; }
        public string VideoUrl { get; set; }
        public string VideoNumber { get; set; }
        public int VideoCategory { get; set; }
    }
}
