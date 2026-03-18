using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HoraCerta.Models;

namespace HoraCerta.Controllers
{
	public class HomeController(ILogger<HomeController> logger) : Controller
	{
		private readonly ILogger<HomeController> _logger = logger;

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Sobre()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
