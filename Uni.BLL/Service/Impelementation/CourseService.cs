using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni.BLL.ModelVM;
using Uni.BLL.ModelVM.Course;
using Uni.BLL.ModelVM.GetDataVM;
using Uni.BLL.Service.Abstraction;
using Uni.DAL.DB;
using Uni.DAL.Entity;
using Uni.DAL.Enum;
using Uni.DAL.Repo.Abstraction;

namespace Uni.BLL.Service.Impelementation
{
    public class CourseService : ICourseService
    {

        private readonly ICourseRepo _repo;

        public CourseService(ICourseRepo repo)
        {
            _repo = repo;
        }

        public async Task<CourseSelectionViewModel> GetCourseSelectionDataAsync()
        {
            var departments = await _repo.GetAllDepartmentsAsync();
            return new CourseSelectionViewModel
            {
                Departments = departments,
                Levels = new List<string> { "Preparatory", "First", "Second", "Third", "Fourth" },
                Semesters = new List<string> { "Fall", "Spring", "Summer" }
            };
        }

        public async Task<List<CoursesVM>> GetCoursesAsync(string department, string level, string semester)
        {
            var courses = await _repo.GetCoursesAsync(department, level, semester);
            return courses.Select(c => new CoursesVM
            {
                CourseCode = c.CourseCode,
                CourseName = c.CourseName,
                CreditHours = c.CreditHours
            }).ToList();
        }

        public async Task<(bool isSuccess, string errorMessage)> SubmitCoursesAsync(string studentId, SubmitCoursesViewModel model)
        {
            var student = await _repo.GetStudentByIdAsync(studentId);
            if (student == null)
                return (false, "Student not found.");

            if (model.SelectedCourseCodes == null || !model.SelectedCourseCodes.Any())
                return (false, "Please select at least one course.");

            var validCourses = new List<Course>();
            int totalCredits = 0;

            foreach (var code in model.SelectedCourseCodes)
            {
                var course = await _repo.GetCourseByCodeAsync(code);
                if (course != null)
                {
                    validCourses.Add(course);
                    totalCredits += course.CreditHours;
                }
            }

            if (totalCredits < 12 || totalCredits > 16)
                return (false, "Total credit hours must be between 12 and 16.");

            foreach (var course in validCourses)
            {
                var takes = new Takes
                {
                    Id = student.Id,
                    CourseCode = course.CourseCode,
                    Semester = course.Semester,
                    Year = course.Year,
                    GPA = 0.0
                };
                await _repo.AddTakesAsync(takes);
            }

            await _repo.SaveAsync();
            return (true, "");
        }
    }

}

