using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.Client.RecordsRepository;
using MiNET.LevelDB;
using MiNET.Utils;
using Uni.BLL.ModelVM;
using Uni.BLL.Service.Abstraction;
using Uni.DAL.Entity;
using Uni.BLL.ModelVM.Data;
using Uni.BLL.ModelVM.Admin;
using Uni.BLL.ModelVM.Account;
using Uni.BLL.ModelVM.GetDataVM;


namespace Uni.PLL.Controllers
{
    public class AdminController(SignInManager<Student> signInManager, UserManager<Student> userManager, IConfiguration configuration, IAccountService userService) : Controller
    {
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
        //public IActionResult Index()
        //{
        //    var users = Studentser.GetAllStudent();
        //    if (User != null)
        //    {
        //        return View(users);
        //    }
        //    return View("AdminDashboard");
        //}
		public async Task<IActionResult> GetALL()
		{
			var StudentProfileVM = await userService.GetAllStudent(User);
			return View(StudentProfileVM);
		}
		[HttpGet]
		//public IActionResult GetStudentJson()
		//{
		//	var StudentProfileVM = userService.GetAllStudent(User);
		//	return Json(StudentProfileVM);
		//}

		public IActionResult GetC()
		{
			var Data = userService.GetAllCourses(); 
			return View(Data); 
		}
		public IActionResult GetD()
		{
			var Data = userService.GetAllDepartments(); 
			return View(Data); 
		}
		public IActionResult GetT()
		{
			var Data = userService.GetAllTakes(); 
			return View(Data); 
		}
		public IActionResult GetI()
		{
			var Data = userService.GetAllInstructors(); 
			return View(Data); 
		}
        public IActionResult GetTeaches()
		{
			var Data = userService.GetAllTeaches();
			return View(Data);
		}
        public IActionResult GetAllRecords()
        {
			var Data = userService.GetAllRecords();
			return View(Data);
		}

		public async Task<IActionResult> GetALLSudent()
        {
            var StudentProfileVM = await userService.GetAllStudent(User);
            var Data = userService.GetAllCourses();
            var dept = userService.GetAllDepartments();
            var takes = userService.GetAllTakes();
            var ins = userService.GetAllInstructors();
            var teaches = userService.GetAllTeaches();
			var records = userService.GetAllRecords();
			var data = new StudentAllData
			{
				Students = StudentProfileVM,
				Courses = Data,
				Departments = dept,
				Takes = takes,
				Instructors = ins,
				Teaches = teaches
			};
            return View(data);
		}
			[HttpGet]
		public async Task<IActionResult> GetStudentJson()
		{
            var StudentProfileVM = await userService.GetAllStudent(User);
			return Json(StudentProfileVM);
		}
		[HttpGet]
		public IActionResult GetCoursesJson()
		{
			var Data = userService.GetAllCourses();
			return Json(Data);
		}
		[HttpGet]
		public IActionResult GetDepartmentJson()
		{
			var Data = userService.GetAllDepartments();
			return Json(Data);
		}
        public IActionResult GetTakesJson()
        {
			var Data = userService.GetAllTakes();
			return Json(Data);
		}
        public IActionResult GetInstructorsJson()
        {
			var Data = userService.GetAllInstructors();
			return Json(Data);
		}
		public IActionResult GetTeachesJson()
		{
			var Data = userService.GetAllTeaches();
			return Json(Data);
		}
		public IActionResult GetCourses()
		{
			return View();
		}
        public IActionResult AdminDashboard()
		{
			return View();
		}

	}
}
