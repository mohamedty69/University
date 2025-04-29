using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uni.DAL.Entity
{
    public class Course
    {
        [Key]
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; } = 0;
        public string? Description { get; set; }
        public string Semester { get; set; }
        public string Year { get; set; }
        [ForeignKey("Department")]
        public string DeptName{ get; set; }
        public virtual Department Department { get; set; }

        public ICollection<Takes> Takes { get; set; }
        public ICollection<Teaches> Teaches { get; set; }
        [InverseProperty("MainCourse")]
        public virtual ICollection<Prerequisites> Prerequisites { get; set; }

        [InverseProperty("PrerequisiteCourse")]
        public virtual ICollection<Prerequisites> IsPrerequisiteFor { get; set; }

      
     

    }
}
