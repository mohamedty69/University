using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.DAL.Entity
{
    public class Rcords
    {
        [Key]
        public int recordId { get; set; }
        [ForeignKey("Student")]
        public string Id { get; set; }
        public string CourseCode { get; set; }
        public double  GPA { get; set; }
        public bool Improved { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }

        public ICollection<Student> Student { get; set; }
    }
}
