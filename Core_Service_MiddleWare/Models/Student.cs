using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Service_MiddleWare.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Display(Name ="年纪")]
        public int Age { get; set; }
        [Display(Name = "名字"), Required, MaxLength(10)]
        public string Name { get; set; }
        [Display(Name = "生日")]
        public DateTime BirthTime { get; set; }
        [Display(Name = "性别")]
        public Gender gender { get; set; }
    }
}
