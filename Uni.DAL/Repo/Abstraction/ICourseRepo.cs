using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Uni.DAL.Enum;
using Uni.DAL.Entity;


namespace Uni.DAL.Repo.Abstraction
{
    public interface ICourseRepo
    {
        Task<List<Course>> GetCoursesAsync(string department, string year, string semester);
        Task SaveTakesAsync(List<Takes> takes);
        Task<int> GetTotalCreditsAsync(List<string> courseCodes);

        //List<Course> GetCourses(string department, string semester, string year);
        //List<Course> GetCoursesByCodes(List<string> codes);
        //void AddTake(Takes take);
        //void SaveChanges();
    }
}
