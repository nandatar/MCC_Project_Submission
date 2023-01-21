using API.Contexts;
using API.Models;

namespace API.Repositories.Data;

public class ParticipantRepositories : GeneralRepository<MyContext, Participant, string>
{
	public ParticipantRepositories(MyContext context) : base(context)
	{
	}
}
