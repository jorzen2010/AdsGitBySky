using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdsEntity
{
    public class AdsTeacher
    {
        [Key]
        public int TeacherId { get; set; }
        public int CustomerId { get; set; }

         [Display(Name = "教师简介")]
        public string TeacherInfo { get; set; }
         [Display(Name = "教师学历")]
        public string TeacherXueli { get; set; }




    }
}
