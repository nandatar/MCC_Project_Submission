using API.Base;
using API.Contexts;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Azure.Core;
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
    //public async Task<IActionResult> Upload([FromForm] SubmitProjectVM submitProject)
    //{
    //    var UMLStream = new MemoryStream();
    //    var BPMNStream = new MemoryStream();

    //    await submitProject.UML.CopyToAsync(UMLStream);
    //    await submitProject.BPMN.CopyToAsync(BPMNStream);
    //    var project = new Project
    //    {
    //        ProjectTitle = submitProject.Title,
    //        Description = submitProject.Description,
    //        CurrentStatus = 1,
    //        UML = UMLStream.ToArray(),
    //        BPMN = BPMNStream.ToArray(),
    //        Link = submitProject.Link
    //    };
    //    _context.Projects.Add(project);
    //    await _context.SaveChangesAsync();
    //    return Ok();
    //}
    public ActionResult Submit([FromForm] SubmitProjectVM submitProject)
    {
        try
        {
            var result = _repo.Submit(submitProject);
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
    [Route("Edit")]
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

}

