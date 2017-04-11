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
        public string TeacherName { get; set; }
        public string TeacherSex { get; set; }
        public string TeacherBirthday { get; set; }
        public string TeacherInfo { get; set; }
        public string TeacherXueli { get; set; }
        public int TeacherOrg { get; set; }
    }
}
