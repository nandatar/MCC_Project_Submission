using API.Contexts;
using API.Models;

namespace API.Repositories.Data;

public class EmployeeRepositories : GeneralRepository<MyContext, Employee, string>
{
	public EmployeeRepositories(MyContext context) : base(context)
	{
	}
}
