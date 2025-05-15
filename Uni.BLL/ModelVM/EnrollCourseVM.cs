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
        public List<string> SelectedCourses { get; set; }
        public List<Course> AvailableCourses { get; set; }
        public string SelectedDept { get; set; }
    

        public List<string> SelectedCourseCodes { get; set; } = new();
        public int TotalCreditHours { get; set; }
    }
    public class CourseItem
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CreditHours { get; set; }
        public bool IsSelected { get; set; }
    }

}
