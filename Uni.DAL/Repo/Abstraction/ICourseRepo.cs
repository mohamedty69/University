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

        List<Course> GetCourses(string department, string semester, string year);
        List<Course> GetCoursesByCodes(List<string> codes);
        void AddTake(Takes take);
        void SaveChanges();
    }
}
