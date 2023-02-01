﻿using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
            HttpContext.Session.SetString("JWToken", token);
			TempData["message"] = "Login Berhasil";

            return RedirectToAction("Index");

		}
        public IActionResult Index()
        {
			if (HttpContext.Session.GetString("JWToken") != null)
            {
				try
				{
					string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
					var handler = new JwtSecurityTokenHandler();
					var jwtSecurityToken = handler.ReadJwtToken(jwt);
					var payload = jwtSecurityToken.Claims;
					string role = payload.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
					if (role == "Participant")
					{
						return RedirectToAction("index", "Participant");
					}
					return RedirectToAction("index", "Trainer");
				}
				catch (Exception)
				{

					return View();
				}
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Landing()
        {
			//get user from jwt payload
			string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
			var handler = new JwtSecurityTokenHandler();
			var jwtSecurityToken = handler.ReadJwtToken(jwt);
			var payload = jwtSecurityToken.Claims;
			string role = payload.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
			if (role == "Participant")
			{
				return RedirectToAction("index", "Participant");
			}
			return RedirectToAction("index", "Trainer");
		}

        [HttpGet("Register/")]
        public IActionResult Register()
        {
            return View("Register");
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
            return RedirectToAction("index", "register");
        }
    }
}
