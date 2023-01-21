using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EmployeesController : BaseController<EmployeeRepositories, Employee, string>
{
	public EmployeesController(EmployeeRepositories repo) : base(repo)
	{
	}

}
