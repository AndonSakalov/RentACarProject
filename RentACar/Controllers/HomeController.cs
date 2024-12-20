using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Models;
using System.Diagnostics;

namespace RentACar.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly RentACarDbContext context;

		public HomeController(ILogger<HomeController> logger, RentACarDbContext context)
		{
			_logger = logger;
			this.context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		[Route("Home/Error/{statusCode?}")]
		public IActionResult Error(int? statusCode)
		{
			if (statusCode == 404)
			{
				return View("404");
			}
			if (statusCode == 500)
			{
				return View("500");
			}
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
