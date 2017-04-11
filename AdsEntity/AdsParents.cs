using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    
    public class AdsParents
    {
        [Key]
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentSex { get; set; }
        public DateTime ParentBirthday { get; set; }
        public int ParentCategory { get; set; }

    }
}
