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
        public string  PlanContent { get; set; }
        public DateTime PlanTime { get; set; }
    }

}
