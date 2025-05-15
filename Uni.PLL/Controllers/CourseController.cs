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
    public class CourseController : Controller
    {
        private readonly ICourseService _service;
        private readonly UserManager<Student> _userManager;

        public CourseController(ICourseService service, UserManager<Student> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> SelectCourses(string department, Year year, Semester semester)
        {
            var courses = await _service.GetAvailableCoursesAsync(department, year.ToString(), semester.ToString());
            var vm = new EnrollCourseVM
            {
                DeptName = department,
                SelectedYear = year,
                SelectedSemester = semester,
                AvailableCourses = courses
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollCourses(EnrollCourseVM vm)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _service.EnrollStudentAsync(userId, vm.SelectedCourses, vm.SelectedSemester.ToString(), vm.SelectedYear.ToString());
            if (!success)
            {
                ModelState.AddModelError("", "Total credit hours must be at least 12");
                vm.AvailableCourses = await _service.GetAvailableCoursesAsync(vm.DeptName, vm.SelectedYear.ToString(), vm.SelectedSemester.ToString());
                return View("SelectCourses", vm);
            }
            return RedirectToAction("Success");
        }

        public IActionResult Success() => View();



        //private readonly ICourseService _courseService;
        //private readonly UserManager<Student> _userManager;

        //public CourseController(ICourseService courseService, UserManager<Student> userManager)
        //{
        //    _courseService = courseService;
        //    _userManager = userManager;
        //}

        //public IActionResult SelectCriteria()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ShowCourses(string department, int level, Semester semester, Year year)
        //{
        //    var courses = _courseService.GetAvailableCourses(department, semester.ToString(), year.ToString());

        //    var vm = new CourseEnrollVM
        //    {
        //        Department = department,
        //        Level = level,
        //        Semester = semester,
        //        Year = year,
        //        AvailableCourses = courses
        //    };

        //    return View(vm);
        //}

        //[HttpPost]
        //public async Task<IActionResult> SubmitCourses(CourseEnrollVM model)
        //{
        //    string studentId = _userManager.GetUserId(User);
        //    bool success = await _courseService.EnrollCoursesAsync(model, studentId);

        //    if (success)
        //    {
        //        TempData["Message"] = "Enrollment successful!";
        //        return RedirectToAction("EnrollmentSuccess");
        //    }

        //    TempData["Error"] = "You must choose at least 12 credit hours!";
        //    return RedirectToAction("ShowCourses", new
        //    {
        //        department = model.Department,
        //        level = model.Level,
        //        semester = model.Semester,
        //        year = model.Year
        //    });
        //}

        //public IActionResult EnrollmentSuccess()
        //{
        //    return View();
        //}
    }
}
