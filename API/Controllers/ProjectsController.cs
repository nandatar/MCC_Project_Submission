using API.Base;
using API.Contexts;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers;

public class ProjectsController : BaseController<ProjectRepositories, Project, int>
{
    private ProjectRepositories _repo;
    private readonly MyContext _context;
    public ProjectsController(ProjectRepositories repo,  MyContext context) : base(repo)
    {
        _repo = repo;
        _context = context;
    }

	[HttpPost]
	[Route("submit")]
	public ActionResult Submit(SubmitVM submit)
	{
		try
		{
			var result = _repo.Submit(submit);
			return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
			Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
		}
		catch
		{
			return BadRequest(new { statusCode = 500, message = "" });
		}
	}

	[HttpPut]
	[Route("Edit")]
	public ActionResult Edit(SubmitVM submit)
	{
		try
		{
			var result = _repo.Edit(submit);
			return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
			Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
		}
		catch
		{
			return BadRequest(new { statusCode = 500, message = "" });
		}
	}


	[HttpPost]
    [Route("submitproject")]
    public ActionResult SubmitProject([FromForm] SubmitProjectVM submitProject)
    {
        try
        {
            var result = _repo.SubmitProject(submitProject);
            return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
            Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 500, message = "" });
        }
    }

    [HttpGet]
    [Route("UML/{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        var image = await _context.Projects.FindAsync(id);
        if (image == null)
        {
            return NotFound();
        }
        return File(image.UML, "image/png");
    }

    [HttpGet]
    [Route("BPMN/{id}")]
    public async Task<IActionResult> GetImage_(int id)
    {
        var image = await _context.Projects.FindAsync(id);
        if (image == null)
        {
            return NotFound();
        }
        return File(image.BPMN, "image/png");
    }

    [HttpPut]
    [Route("EditProject")]
    public ActionResult EditProject([FromForm] EditProjectVM editProject)
    {
        try
        {
            var result = _repo.EditProject(editProject);
            return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Update" }) :
            Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
        }
        catch(Exception e)
        {
            return BadRequest(new { statusCode = 500, message = e });
        }
    }

    [HttpGet]
    [Route("Master")]
    [AllowAnonymous]
    public ActionResult GetMaster()
    {
        try
        {
            var result = _repo.MasterProject();
            return result.Count() == 0
            ? Ok(new { statusCode = 204, message = "Data Not Found!" })
            : Ok(new { statusCode = 201, message = "Success", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }

    }

	[HttpGet]
	[Route("Master/{id}")]
	public ActionResult GetMaster(int id)
    {
		try
		{
			var result = _repo.MasterProject(id);
			return result.Count() == 0
			? Ok(new { statusCode = 204, message = "Data Not Found!" })
			: Ok(new { statusCode = 201, message = "Success", data = result });
		}
		catch (Exception e)
		{
			return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
		}
	}

    [HttpGet]
    [Route("Master/Score")]
    public ActionResult MasterProjectScore()
    {
        try
        {
            var result = _repo.MasterProjectScore();
            return result.Count() == 0
            ? Ok(new { statusCode = 204, message = "Data Not Found!" })
            : Ok(new { statusCode = 201, message = "Success", data = result });
        }
        catch (Exception e)
        {
            return BadRequest(new { statusCode = 500, message = $"Something Wrong! : {e.Message}" });
        }
    }

	[HttpPost]
	[Route("Final")]
	public ActionResult Finalizaiton(FinalVM final)
	{
		try
		{
			var result = _repo.Finalization(final);
			return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
			Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
		}
		catch
		{
			return BadRequest(new { statusCode = 500, message = "" });
		}
	}

	[HttpPost]
	[Route("Score")]
	public ActionResult Score(ScoreVM score)
	{
		try
		{
			var result = _repo.Score(score);
			return result == 0 ? Ok(new { statusCode = 204, message = "Data failed to Insert!" }) :
			Ok(new { statusCode = 201, message = "Data Saved Succesfully!" });
		}
		catch
		{
			return BadRequest(new { statusCode = 500, message = "" });
		}
	}
}

