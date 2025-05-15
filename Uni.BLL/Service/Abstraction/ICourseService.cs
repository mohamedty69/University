using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.ModelVM;
using Uni.DAL.Entity;


namespace Uni.BLL.Service.Abstraction
{
  
    public interface ICourseService
    {
        List<Course> GetAvailableCourses(string department, string semester, string year);
        Task<bool> EnrollCoursesAsync(EnrollCourseVM model, string studentId);

        List<Course> GetCourses(string dept, string semester, string year);
    }

}
