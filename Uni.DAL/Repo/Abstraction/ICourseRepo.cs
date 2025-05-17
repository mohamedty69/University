using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Uni.DAL.Enum;
using Uni.DAL.Entity;

using Uni.DAL.Enum;


namespace Uni.DAL.Repo.Abstraction
{
    public interface ICourseRepo
    {

        Task<List<Department>> GetAllDepartmentsAsync();
        Task<List<Course>> GetCoursesAsync(string department, string level, string semester);
        Task<Course?> GetCourseByCodeAsync(string code);
        Task<Student?> GetStudentByIdAsync(string id);
        Task AddTakesAsync(Takes takes);
        Task SaveAsync();
    }
}
