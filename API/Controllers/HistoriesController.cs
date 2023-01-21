using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HistoriesController : BaseController<HistoryRepositories, History, int>
{
	public HistoriesController(HistoryRepositories repo) : base(repo)
	{
	}

}
