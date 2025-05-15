using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.ModelVM;
using Uni.BLL.Service.Abstraction;
using Uni.DAL.Entity;
using Uni.DAL.Enum;
using Uni.DAL.Repo.Abstraction;

namespace Uni.BLL.Service.Impelementation
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _repo;
        public CourseService(ICourseRepo repo) => _repo = repo;

        public async Task<List<Course>> GetAvailableCoursesAsync(string department, string year, string semester) =>
            await _repo.GetCoursesAsync(department, year, semester);

        public async Task<bool> EnrollStudentAsync(string studentId, List<string> courseCodes, string semester, string year)
        {
            int totalCredits = await _repo.GetTotalCreditsAsync(courseCodes);
            if (totalCredits < 12) return false;

            var takes = courseCodes.Select(code => new Takes
            {
                Id = studentId,
                CourseCode = code,
                Semester = semester,
                Year = year,
                GPA = 0
            }).ToList();

            await _repo.SaveTakesAsync(takes);
            return true;
        }

        //private readonly ICourseRepo _courseRepo;

        //public CourseService(ICourseRepo courseRepo)
        //{
        //    _courseRepo = courseRepo;
        //}

        //public List<DAL.Entity.Course> GetAvailableCourses(string department, string semester, string year)
        //{
        //    return _courseRepo.GetCourses(department, semester, year);
        //}

        //public async Task<bool> EnrollCoursesAsync(EnrollCourseVM model, string studentId)
        //{
        //    var selectedCourses = _courseRepo.GetCoursesByCodes(model.SelectedCourseCodes);
        //    int totalHours = selectedCourses.Sum(c => c.CreditHours);

        //    if (totalHours >= 12)
        //    {
        //        foreach (var course in selectedCourses)
        //        {
        //            _courseRepo.AddTake(new Takes
        //            {
        //                Id = studentId,
        //                CourseCode = course.CourseCode,
        //                Semester = model.Semester.ToString(),
        //                Year = model.Year.ToString(),
        //                GPA = 0.0
        //            });
        //        }

        //        _courseRepo.SaveChanges();
        //        return true;
        //    }

        //    return false;
        //}
       
    }
}
