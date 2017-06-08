using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsEntity
{
    public class Pingjia
    {
        public int PingjiaId { get; set; }
        public int BabyId { get; set; }
        public int VideoId { get; set; }
        public PingjiaStatus Status { get; set; }
        public DateTime PingjiaTime { get; set; }
    }

    public enum PingjiaStatus
        { 
            熟练完成=1,
            基本完成=2,
            不能完成=3
        }
}
