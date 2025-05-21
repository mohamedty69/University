using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Uni.BLL.Service.Abstraction;
using Uni.PLL.Models;

namespace Uni.PLL.Controllers
{
    public class HomeController : Controller
    {
		private readonly IFastApiService _fastApiService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IFastApiService fastApiService)
        {
            _logger = logger;
			_fastApiService = fastApiService ?? throw new ArgumentNullException(nameof(fastApiService));
		}
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Dashprofile() 
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Dashprofile(string file)
		{
			var apiResponse = await _fastApiService.UploadStringAsync(file);
			Console.WriteLine(apiResponse);
			// Display response
			return Content(apiResponse);
		}
    }
}
