using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	[Authorize(Roles = "Trainer")]
	public class TrainerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[HttpGet("Review/")]
		public IActionResult Review()
        {
            return View();
        }

		[HttpGet("Score/")]
		public IActionResult Score()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult RemoveAllSession()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Login");
		}
	}
}
