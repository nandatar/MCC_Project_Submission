using API.Contexts;
using API.Models;

namespace API.Repositories.Data;

public class HistoryRepositories : GeneralRepository<MyContext, History, int>
{
	public HistoryRepositories(MyContext context) : base(context)
	{
	}
}

