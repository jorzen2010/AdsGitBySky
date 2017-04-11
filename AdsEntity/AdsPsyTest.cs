using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsPsyTest
    {
        [Key]
        public int PsyTestId { get; set; }
        public int BabyId { get; set; }
        public int ParentId { get; set; }
        public string TestKey { get; set; }
        public string TestResult { get; set; }
    }
}
