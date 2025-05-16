using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.ModelVM;
using Uni.BLL.ModelVM.Course;
using Uni.BLL.ModelVM.GetDataVM;
using Uni.DAL.Entity;


namespace Uni.BLL.Service.Abstraction
{
  
    public interface ICourseService
    {
        Task<CourseSelectionViewModel> GetCourseSelectionDataAsync();
        Task<List<CoursesVM>> GetCoursesAsync(string department, string level, string semester);
        Task<(bool isSuccess, string errorMessage)> SubmitCoursesAsync(string studentId, SubmitCoursesViewModel model);
    }

}
