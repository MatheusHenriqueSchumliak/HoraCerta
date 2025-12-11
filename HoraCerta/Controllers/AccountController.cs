using Microsoft.AspNetCore.Mvc;

namespace HoraCerta.Controllers
{
	public class AccountController : Controller
	{
		public AccountController()
		{

		}

		public IActionResult Login()
		{
			return View();
		}
	}
}
