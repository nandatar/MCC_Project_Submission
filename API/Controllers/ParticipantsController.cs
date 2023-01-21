using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ParticipantsController : BaseController<ParticipantRepositories, Participant, string>
{
	public ParticipantsController(ParticipantRepositories repo) : base(repo)
	{
	}

}
