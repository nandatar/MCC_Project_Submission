using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StatusController : BaseController<StatusRepositories, Status, int>
{
	public StatusController(StatusRepositories repo) : base(repo)
	{
	}

}
