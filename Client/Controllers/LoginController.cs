using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class LoginController : BaseController<Account, LoginRepository, int>
    {
        private readonly LoginRepository repository;
        public LoginController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.generateToken;
            if (token == null)
            {
                TempData["message"] = "Gagal Login";
                return RedirectToAction("index");
            }
            TempData["message"] = "Login Berhasil";
            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("SignUp/")]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            var result = await repository.Register(register);
            var status = result.StatusCode;

            if (status == 201)
            {
                TempData["message"] = "Register Berhasil";
                return RedirectToAction("index");                     
            }

            TempData["message"] = "Register Gagal";
            return RedirectToAction("index", "signup");
        }
    }
}
