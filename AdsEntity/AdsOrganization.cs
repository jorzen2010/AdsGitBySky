using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsOrganization
    {
        [Key]
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string OrgDescription { get; set; }
        public string OrgShengfen { get; set; }
        public string OrgCity { get; set; }

        public string OrgDiqu { get; set; }
        public string OrgAddress { get; set; }
        public string OrgContactName { get; set; }
        public string OrgContactTel { get; set; }
    }
}
