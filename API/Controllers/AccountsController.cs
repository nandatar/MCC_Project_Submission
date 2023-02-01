using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController : BaseController<AccountRepositories, Account, string>
{
	private AccountRepositories _repo;
	private IConfiguration _con;
	public AccountsController(AccountRepositories repo, IConfiguration con) : base(repo)
	{
		_repo = repo;
		_con = con;
	}

	[HttpPost]
	[Route("Register")]
	[AllowAnonymous]
	public ActionResult Register(RegisterVM registerVM)
	{
		
		try
		{
			switch (_repo.Register(registerVM))
			{
				case 0:
					return Ok(new { statusCode = 204, message = "NIK Sudah Dipakai" });
				case 1:
					return Ok(new { statusCode = 204, message = "Email Sudah Dipakai" });
				case 2:
					return Ok(new { statusCode = 201, message = "Username Sudah Dipakai" });
				case 3:
					return Ok(new { statusCode = 201, message = "Register Succesfully!" });
			}
			return BadRequest(new { statusCode = 500, message = "Something Wrong! " });
		}
		catch (Exception e)
		{
			return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
		}
	}

	[HttpPost]
	[Route("Login")]
	[AllowAnonymous]
	public ActionResult Login(LoginVM loginVM)
	{
		try
		{
			var result = _repo.Login(loginVM);
			switch (result)
			{
				case 0:
					return Ok(new { statusCode = 200, message = "Account Not Found!" });
				case 1:
					return Ok(new { statusCode = 200, message = "Wrong Password!" });
				default:
					// bikin method untuk mendapatkan role-nya user yang login
					var roles = _repo.UserRoles(loginVM.Email_Username);
					var nik = _repo.GetNIK(loginVM.Email_Username);

					var claims = new List<Claim>()
					{
						new Claim("email/username", loginVM.Email_Username),
						new Claim("NIK", nik)

					};

					claims.Add(new Claim(ClaimTypes.Role, roles));
					
					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_con["JWT:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var token = new JwtSecurityToken(
						_con["JWT:Issuer"],
						_con["JWT:Audience"],
						claims,
						expires: DateTime.Now.AddMinutes(100),
						signingCredentials: signIn
						);

					var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
					claims.Add(new Claim("Token Security", generateToken.ToString()));

					return Ok(new { statusCode = 200, generateToken, message = "Login Success!" });
			}
		}
		catch (Exception e)
		{
			return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
		}
	}
}
