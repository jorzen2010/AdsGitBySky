﻿using System;
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

        熟练完成=0.3,
        基本完成=0.5,
        不能完成=0.8

    }

    public class PingjiaWeight

    {
        public int PingjiaWeightId { get; set; }
        public int VideoId { get; set; }
        public int BabyId { get; set; }
        public float VideoWeight { get; set; }

    }
}
