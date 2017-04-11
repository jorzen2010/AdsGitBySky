using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsCard
    {
        [Key]
        public int CardId { get; set; }
        public string CardNumber { get; set; }
    }
}
