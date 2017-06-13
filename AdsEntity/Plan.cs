using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace AdsEntity
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }
        public int BabyId { get; set; }
        public string  PlanA { get; set; }
        public string PlanB { get; set; }
        public string PlanC { get; set; }
        public string PlanD { get; set; }
        public string PlanE { get; set; }
        public DateTime PlanTime { get; set; }
    }


    public class PlanOrderWeight
    {
        public int VideoId { get; set; }
        public float OrderValue { get; set; }

    
    }

}
