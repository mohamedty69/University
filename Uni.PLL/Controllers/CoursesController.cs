using Microsoft.AspNetCore.Mvc;

namespace Uni.PLL.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Courses()
        {
            return View();
        }

    }
}
