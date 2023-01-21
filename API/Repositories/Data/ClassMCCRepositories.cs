using API.Contexts;
using API.Models;

namespace API.Repositories.Data;

public class ClassMCCRepositories : GeneralRepository<MyContext, ClassMCC, int>
{
	public ClassMCCRepositories(MyContext context) : base(context)
	{
	}
}

