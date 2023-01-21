using API.Base;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers;

public class ProjectsController : BaseController<ProjectRepositories, Project, int>
{
	public ProjectsController(ProjectRepositories repo) : base(repo)
	{
	}

}
