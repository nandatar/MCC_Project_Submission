using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ClassMCCController : BaseController<ClassMCCRepositories, ClassMCC, int>
{
	public ClassMCCController(ClassMCCRepositories repo) : base(repo)
	{
	}
}
