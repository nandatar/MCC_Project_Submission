using API.Contexts;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

    public int Submit([FromForm] SubmitProjectVM submitProject)
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
}
