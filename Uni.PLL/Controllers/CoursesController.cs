using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uni.DAL.Entity;
using Uni.DAL.Enum;
using Uni.DAL.DB;
using Microsoft.EntityFrameworkCore;
using Uni.BLL.ModelVM.Course;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Uni.BLL.Service.Abstraction;


namespace Uni.PLL.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<Student> _userManager;

        public CoursesController(ICourseService courseService, UserManager<Student> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
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

    }
}
