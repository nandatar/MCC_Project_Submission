using API.Contexts;
using API.Models;
using API.ViewModels;

namespace API.Repositories.Data;

public class HistoryRepositories : GeneralRepository<MyContext, History, int>
{
	private readonly MyContext _context;
	public HistoryRepositories(MyContext context) : base(context)
	{
		_context = context;
	}

	public int PostHistory(ReviewVM reviewvm)
	{
		var review = new History
		{
			ProjectID = reviewvm.ProjectID,
			Time = reviewvm.Time,
			Message = reviewvm.Message,
			Revision = reviewvm.Revision,
			StatusID = reviewvm.StatusID,
		};
		_context.Histories.Add(review);

		//update status di table project
		var entity = _context.Projects.Find(reviewvm.ProjectID);
		entity.CurrentStatus = reviewvm.StatusID;
		
		var result = _context.SaveChanges();
		return result;
	}

	public List<History> GetByIdProject(int id)
	{
		return _context.Histories.Where(h => h.ProjectID == id).ToList();
	}
}

