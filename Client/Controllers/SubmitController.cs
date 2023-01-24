using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	public class SubmitController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
