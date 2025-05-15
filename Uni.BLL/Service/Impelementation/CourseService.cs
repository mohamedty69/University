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

        private readonly ICourseRepo _courseRepo;

        public CourseService(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public List<DAL.Entity.Course> GetAvailableCourses(string department, string semester, string year)
        {
            return _courseRepo.GetCourses(department, semester, year);
        }

        public async Task<bool> EnrollCoursesAsync(EnrollCourseVM model, string studentId)
        {
            var selectedCourses = _courseRepo.GetCoursesByCodes(model.SelectedCourseCodes);
            int totalHours = selectedCourses.Sum(c => c.CreditHours);

            if (totalHours >= 12)
            {
                foreach (var course in selectedCourses)
                {
                    _courseRepo.AddTake(new Takes
                    {
                        Id = studentId,
                        CourseCode = course.CourseCode,
                        Semester = model.SelectedSemester.ToString(),
                        Year = model.SelectedYear.ToString(),
                        GPA = 0.0
                    });
                }

                _courseRepo.SaveChanges();
                return true;
            }

            return false;
        }
        public List<Course> GetCourses(string dept, string semester, string year)
        {
            return _courseRepo.GetCourses(dept, semester, year);
        }

    }
}
