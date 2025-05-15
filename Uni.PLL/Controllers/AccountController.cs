using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Uni.DAL.Entity;
using Uni.BLL.Service.Abstraction;
using Uni.BLL.ModelVM;
namespace Uni.PLL.Controllers
{
    public class AccountController(SignInManager<Student> signInManager, UserManager<Student> userManager, IConfiguration configuration, IAccountService userService) : Controller
    {

        // repo 
        private readonly SignInManager<Student> _signInManager = signInManager;
        private readonly UserManager<Student> _userManager = userManager;
        private readonly IAccountService userService = userService;
        private readonly IConfiguration _configuration = configuration;
        public async Task<IActionResult> Login()
       {
            var loginVM = await userService.GetLoginViewModelAsync();
            return View(loginVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", loginVM);
            }

            var result = await userService.Login(loginVM);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked. Please try again later.");
                return View("Login", loginVM);
            }
            ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
            return View("Login", loginVM);
        }
        //logout
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegistrationVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegistrationVM registerVM)
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

    }
}
