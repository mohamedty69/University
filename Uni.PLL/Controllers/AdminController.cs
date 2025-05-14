using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Uni.BLL.ModelVM;
using Uni.BLL.Service.Abstraction;
using Uni.DAL.Entity;

namespace Uni.PLL.Controllers
{
    public class AdminController(SignInManager<Student> signInManager, UserManager<Student> userManager, IConfiguration configuration, IAccountService userService) : Controller
    {
        private readonly IAccountService Studentser;
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateStudentVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", registerVM);
            }

            var result = await userService.RegisterUserAsync(registerVM);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Register", registerVM);
        }
        public async Task<IActionResult> Edit()
        {
            var model = await userService.GetUserForEdit(User);
            if (model == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await userService.UpdateUser(User, model);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            TempData["SuccessMessage"] = "Your profile has been updated successfully!";
            return RedirectToAction("Profile", "Account");
        }
        public IActionResult Index()
        {
            var users = Studentser.GetAll();
            if (User != null)
            {
                return View(users);
            }
            return View();
        }

    }
}
