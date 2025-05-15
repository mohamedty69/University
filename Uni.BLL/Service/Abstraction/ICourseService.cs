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
        Task<List<Course>> GetAvailableCoursesAsync(string department, string year, string semester);
        Task<bool> EnrollStudentAsync(string studentId, List<string> courseCodes, string semester, string year);

        //List<Course> GetAvailableCourses(string department, string semester, string year);
        //Task<bool> EnrollCoursesAsync(EnrollCourseVM model, string studentId);
    }

}
