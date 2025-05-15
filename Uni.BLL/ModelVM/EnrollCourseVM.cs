using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.DAL.Entity;
using Uni.DAL.Enum;

namespace Uni.BLL.ModelVM
{
    public class EnrollCourseVM
    {
        public string StudentId { get; set; }
        public string DeptName { get; set; }
        public Year SelectedYear { get; set; }
        public Semester SelectedSemester { get; set; }
        public string SelectedCourses { get; set; } // JSON string representing List<CourseVM>

        public List<string> SelectedCourseCodes { get; set; } = new();
        public List<Course> AvailableCourses { get; set; } = new();

    }
 

}
