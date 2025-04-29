using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.DAL.Entity
{
    public class Takes
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Student")]
        public string Id { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Course")]
        public string CourseCode { get; set; }
        [Key, Column(Order = 2)]

        public string Semester { get; set; }
        [Key, Column(Order = 3)]

        public string Year { get; set; }
        public double GPA { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }


    }
}
