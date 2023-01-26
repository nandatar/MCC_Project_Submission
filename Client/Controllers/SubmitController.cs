using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	 public class SubmitController : BaseController<Project, SubmitRepository, int>
    {
        private readonly SubmitRepository repository;
        public SubmitController(SubmitRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] SubmitProjectVM submitProject)
        {
            var result = await repository.Submit(submitProject);
            var status = result.StatusCode;

            if (status == 201)
            {
                TempData["message"] = "Submit Berhasil";
                return RedirectToAction("index");
            }

            TempData["message"] = "Submit Gagal";
            return RedirectToAction("index");
        }
    }
}
