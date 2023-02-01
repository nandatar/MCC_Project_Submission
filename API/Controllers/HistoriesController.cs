using API.Base;
using API.Contexts;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HistoriesController : BaseController<HistoryRepositories, History, int>
{
	private HistoryRepositories _repo;
	private readonly MyContext _context;
	public HistoriesController(HistoryRepositories repo, MyContext context) : base(repo)
	{
		_repo = repo;
		_context = context;
	}

	[HttpPost]
	[Route("PostHistory")]
	public ActionResult Submit(ReviewVM review)
	{
		try
		{
			var result = _repo.PostHistory(review);
			return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
			Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
		}
		catch(Exception e)
		{
			return BadRequest(new { statusCode = 500, message = e });
		}
	}

	[HttpGet]
	[Route("Project/{id}")]
	[AllowAnonymous]
	public ActionResult GetByIdProject(int id)
	{
		try
		{
			var result = _repo.GetByIdProject(id);
			return result.Count() == 0
			? Ok(new { statusCode = 204, message = "Data Not Found!" })
			: Ok(new { statusCode = 201, message = "Success", data = result });
		}
		catch (Exception e)
		{
			return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
		}

	}

}
