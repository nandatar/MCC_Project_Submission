using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repositories.Data;

public class ProjectRepositories : GeneralRepository<MyContext, Project, int>
{
	private readonly MyContext _context;
	public ProjectRepositories(MyContext context) : base(context)
	{
		_context = context;
	}

	public int Submit(SubmitProjectVM submitProject)
	{
		Project project = new Project()
		{
			ProjectTitle = submitProject.Title,
			Description = submitProject.Description,
			CurrentStatus = 1
		};
		return 0; //Test
	}
}
