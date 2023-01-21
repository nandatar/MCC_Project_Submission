using API.Contexts;
using API.Models;

namespace API.Repositories.Data;

public class StatusRepositories : GeneralRepository<MyContext, Status, int>
{
	public StatusRepositories(MyContext context) : base(context)
	{
	}
}
