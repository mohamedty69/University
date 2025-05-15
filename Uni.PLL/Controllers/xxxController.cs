using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Uni.BLL.ModelVM;
using Uni.BLL.Service.Abstraction;
using Uni.DAL.DB;
using Uni.DAL.Entity;
using Uni.DAL.Enum;

namespace Uni.PLL.Controllers
{
    public class xxxController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<Student> _userManager;

        public xxxController(ICourseService courseService, UserManager<Student> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }

        public IActionResult SelectCriteria()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowCourses(string department, Semester semester, Year year)
        {
            var courses = _courseService.GetAvailableCourses(department, semester.ToString(), year.ToString());

            var vm = new EnrollCourseVM
            {
                DeptName = department,
                SelectedSemester = semester,
                SelectedYear = year,
                AvailableCourses = courses
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCourses(EnrollCourseVM model)
        {
            string studentId = _userManager.GetUserId(User);
            bool success = await _courseService.EnrollCoursesAsync(model, studentId);

            if (success)
            {
                TempData["Message"] = "Enrollment successful!";
                return RedirectToAction("EnrollmentSuccess");
            }

            TempData["Error"] = "You must choose at least 12 credit hours!";
            return RedirectToAction("ShowCourses", new
            {
                department = model.DeptName,
                semester = model.SelectedSemester,
                year = model.SelectedYear
            });
        }

        public IActionResult EnrollmentSuccess()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCourses(string department, string semester, string year)
        {
            var courses = _courseService.GetAvailableCourses(department, semester, year);
            return Json(courses);
        }
        [HttpGet]
        public IActionResult GetAvailableCourses(string department, string semester, string year)
        {
            try
            {
                var courses = _courseService.GetAvailableCourses(department, semester, year);
                return Json(courses.Select(c => new {
                    c.CourseName,
                    c.CreditHours
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        public IActionResult EnrollCourses()
        {
            return View();
        }
    }
}





