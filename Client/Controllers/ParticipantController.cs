using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Client.Controllers;

[Authorize(Roles = "Participant")]
public class ParticipantController : BaseController<Project, ParticipantRepository, int>
{
	private readonly ParticipantRepository repository;
	public ParticipantController(ParticipantRepository repository) : base(repository)
	{
		this.repository = repository;
	}
	public IActionResult Index()
	{
		string jwt = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
		var handler = new JwtSecurityTokenHandler();
		var jwtSecurityToken = handler.ReadJwtToken(jwt);
		var payload = jwtSecurityToken.Claims;
		string role = payload.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
		string nik = payload.FirstOrDefault(c => c.Type == "NIK").Value;
		var cookie = new CookieOptions
		{
			HttpOnly = false,
			Secure = false,
		};

		// Add the JWT to the cookie
		Response.Cookies.Append("nik", nik, cookie);
		return View();
	}


	[HttpGet("Submit/")]
	public IActionResult Submit()
	{
		return View("Submit");
	}

	[HttpPost]
	public async Task<IActionResult> Submit([FromForm] SubmitProjectVM submitProject)
	{
		//get nik from jwt payload
		string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
		var handler = new JwtSecurityTokenHandler();
		var jwtSecurityToken = handler.ReadJwtToken(jwtToken);
		var payload = jwtSecurityToken.Claims;
		string nik = payload.FirstOrDefault(c => c.Type == "NIK").Value;
		submitProject.NIK = nik;

		var result = await repository.Submit(submitProject);
		var status = result.StatusCode;

		if (status == 201)
		{
			TempData["message"] = "Submit Project Success";
			return RedirectToAction("index");
		}

		TempData["message"] = "Submit Failed";
		return RedirectToAction("index");
	}

	[HttpPost]
	public async Task<IActionResult> Edit([FromForm] SubmitProjectVM submitProject)
	{
		//get nik from jwt payload
		string jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
		var handler = new JwtSecurityTokenHandler();
		var jwtSecurityToken = handler.ReadJwtToken(jwtToken);
		var payload = jwtSecurityToken.Claims;
		string nik = payload.FirstOrDefault(c => c.Type == "NIK").Value;
		submitProject.NIK = nik;

		var result = await repository.Edit(submitProject);
		var status = result.StatusCode;

		if (status == 201)
		{
			TempData["message"] = "Succes Update Project";
			return RedirectToAction("myproject", "participant");
		}

		TempData["message"] = "Failed to update";
		return RedirectToAction("myproject", "participant");
	}

	[HttpGet("MyProject/")]
	public IActionResult MyProject()
	{
		return View("MyProject");
	}

	public IActionResult RemoveAllSession()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Login");
	}
}

	

