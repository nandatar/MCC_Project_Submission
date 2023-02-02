using API.Contexts;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Repositories.Data;

public class ProjectRepositories : GeneralRepository<MyContext, Project, int>
{
    private readonly MyContext _context;
    public ProjectRepositories(MyContext context) : base(context)
    {
        _context = context;
    }

    //public int Submit(SubmitProjectVM submitProject)
    //{
    //	Project project = new Project()
    //	{
    //		ProjectTitle = submitProject.Title,
    //		Description = submitProject.Description,
    //		CurrentStatus = 1,
    //	};
    //	return 0; //Test
    //}
    public int Submit(SubmitVM submit)
    {
        var project = new Project
        {
            ProjectTitle = submit.ProjectTitle,
            Description = submit.Description,
            UML = submit.UML,
            BPMN = submit.BPMN,
            Link = submit.Link,
            CurrentStatus = 1,
		};
		_context.Projects.Add(project);
		_context.SaveChanges();

        var lastID = project.ID;
		var participant_up = _context.Participants.FirstOrDefault(x => x.NIK == submit.NIK);
        participant_up.ProjectID = lastID;
		_context.Participants.Update(participant_up);
		_context.SaveChanges();

		return 1;
    }

	public int Edit(SubmitVM submit)
	{
		var participant= _context.Participants.FirstOrDefault(x => x.NIK == submit.NIK);
		var project = new Project
		{
            ID = participant.ProjectID,
			ProjectTitle = submit.ProjectTitle,
			Description = submit.Description,
			UML = submit.UML,
			BPMN = submit.BPMN,
			Link = submit.Link,
			CurrentStatus = 1,
		};
		_context.Projects.Update(project);
		_context.SaveChanges();

		return 1;
	}
	public int SubmitProject([FromForm] SubmitProjectVM submitProject)
    {
        var UMLStream = new MemoryStream();
        var BPMNStream = new MemoryStream();

        submitProject.UML.CopyToAsync(UMLStream);
        submitProject.BPMN.CopyToAsync(BPMNStream);

        var project = new Project
        {
            ProjectTitle = submitProject.Title,
            Description = submitProject.Description,
            CurrentStatus = 1,
            UML = UMLStream.ToArray(),
            BPMN = BPMNStream.ToArray(),
            Link = submitProject.Link
        };
        _context.Projects.Add(project);
        var result = _context.SaveChanges();
        return result;
    }

    public int EditProject([FromForm] EditProjectVM editProject)
    {
        
        var UMLStream = new MemoryStream();
        var BPMNStream = new MemoryStream();
        editProject.UML.CopyToAsync(UMLStream);
        editProject.BPMN.CopyToAsync(BPMNStream);
        var project = new Project
        {
            ID = editProject.ID,
            ProjectTitle = editProject.ProjectTitle,
            Description = editProject.Description,
            UML = UMLStream.ToArray(),
            BPMN = BPMNStream.ToArray(),
            Link = editProject.Link,
            CurrentStatus = editProject.CurrentStatus,
        };

        _context.Projects.Entry(project).State = EntityState.Modified;
        var result = _context.SaveChanges();
        return result;  
    }

    public IEnumerable<MProjectVM> MasterProject()
    {
        var results = (from p in _context.Projects
                       join pa in _context.Participants on p.ID equals pa.ProjectID
                       join e in _context.Employees on pa.NIK equals e.NIK
                       join c in _context.ClassMCC on e.ClassID equals c.ID     
                       join s in _context.Status on p.CurrentStatus equals s.ID
                       select new MProjectVM
                       {
                           ID = p.ID,
                           ProjectTitle = p.ProjectTitle,
                           Description = p.Description,
                           UML = p.UML,
                           BPMN = p.BPMN,
                           Link = p.Link,
                           StatusName = s.Name,
                           NIK = pa.NIK,
                           Batch = pa.Batch,
                           Status_MCC = pa.Status_MCC,
                           Name = e.Name,
                           Email = e.Email,
                           Position = e.Position,
                           ClassName = c.Name
                       }).ToList();

        return results;
    }

	public IEnumerable<MProjectVM> MasterProject(int id)
	{
		var results = (from p in _context.Projects
					   join pa in _context.Participants on p.ID equals pa.ProjectID
					   join e in _context.Employees on pa.NIK equals e.NIK
					   join c in _context.ClassMCC on e.ClassID equals c.ID
					   join s in _context.Status on p.CurrentStatus equals s.ID
					   where p.ID == id
					   select new MProjectVM
					   {
						   ID = p.ID,
						   ProjectTitle = p.ProjectTitle,
						   Description = p.Description,
						   UML = p.UML,
						   BPMN = p.BPMN,
						   Link = p.Link,
						   StatusName = s.Name,
						   NIK = pa.NIK,
						   Batch = pa.Batch,
						   Status_MCC = pa.Status_MCC,
						   Name = e.Name,
						   Email = e.Email,
						   Position = e.Position,
						   ClassName = c.Name
					   }).ToList();

		return results;
	}

    public IEnumerable<MProjectVM> MasterProjectScore()
    {
        var results = (from p in _context.Projects
                       join pa in _context.Participants on p.ID equals pa.ProjectID
                       join e in _context.Employees on pa.NIK equals e.NIK
                       join c in _context.ClassMCC on e.ClassID equals c.ID
                       join s in _context.Status on p.CurrentStatus equals s.ID
                       where p.CurrentStatus == 5
                       select new MProjectVM
                       {
                           ID = p.ID,
                           ProjectTitle = p.ProjectTitle,
                           Description = p.Description,
                           UML = p.UML,
                           BPMN = p.BPMN,
                           Link = p.Link,
                           StatusName = s.Name,
                           NIK = pa.NIK,
                           Batch = pa.Batch,
                           Status_MCC = pa.Status_MCC,
                           Name = e.Name,
                           Email = e.Email,
                           Position = e.Position,
                           ClassName = c.Name
                       }).ToList();

        return results;
    }

    public int Finalization(FinalVM final)
    {
		var entity = _context.Projects.Find(final.ID);
		entity.CurrentStatus = 5;
        entity.Link = final.Link;

		var result = _context.SaveChanges();
		return result;
	}

	public int Score(ScoreVM score)
	{
		var entity = _context.Projects.Find(score.ID);
		entity.Score = score.Score;
        entity.CurrentStatus = 6;

		var result = _context.SaveChanges();
		return result;
	}
}
