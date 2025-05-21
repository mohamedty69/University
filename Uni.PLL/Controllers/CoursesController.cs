using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uni.DAL.Entity;
using Uni.DAL.Enum;
using Uni.DAL.DB;
using Microsoft.EntityFrameworkCore;
using Uni.BLL.ModelVM.Course;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Uni.BLL.Service.Abstraction;
using Uni.BLL.ModelVM.GetDataVM;


namespace Uni.PLL.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<Student> _userManager;
        private readonly IAccountService userService;

        public CoursesController(ICourseService courseService, UserManager<Student> userManager, IAccountService userService)
        {
            _courseService = courseService;
            _userManager = userManager;
            this.userService = userService;
        }

        public async Task<IActionResult> Select()
        {
            var viewModel = await _courseService.GetCourseSelectionDataAsync();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses(string department, string level, string semester)
        {
            if (string.IsNullOrEmpty(department) || string.IsNullOrEmpty(level) || string.IsNullOrEmpty(semester))
                return BadRequest("Missing parameters");

            var courses = await _courseService.GetCoursesAsync(department, level, semester);
            return Json(courses);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCourses([FromBody] SubmitCoursesViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _courseService.SubmitCoursesAsync(userId, model);

            if (!result.isSuccess)
            {
                ModelState.AddModelError("", result.errorMessage);
                return RedirectToAction("Select");
            }

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

        public async Task<IActionResult> GetT()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Dashprofile", "Home");
            }

            var userWithTakes = await _userManager.Users
                .Where(u => u.Id == user.Id)
                .Include(u => u.Takes)
                .FirstOrDefaultAsync();

            if (userWithTakes == null)
            {
                return RedirectToAction("Dashprofile", "Home");
            }

            // Map Takes to TakesVM
            var takeVMs = userWithTakes.Takes.Select(t => new TakesVM
            {
                CourseCode = t.CourseCode,
                GPA = t.GPA,
                semester = t.Semester,
                year = t.Year
            }).ToList();

            return View(takeVMs); // ✔️ Now you're passing the correct model type
        }

    }
}
