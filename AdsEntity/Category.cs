using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdsEntity
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryInfo { get; set; }
        public int CategoryParentID { get; set; }
        public bool CategoryStatus { get; set; }
        public int CategorySort { get; set; }

        [NotMapped]
        public string CategoryParentName { get; set; }
        [NotMapped]
        public List<Category> ChildCategory { get; set; }
    }
}
