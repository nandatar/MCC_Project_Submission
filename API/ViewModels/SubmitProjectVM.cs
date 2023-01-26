namespace API.ViewModels;

public class SubmitProjectVM
{
	public string Title { get; set; }
	public string Description { get; set; }
	public IFormFile UML { get; set; }
	public IFormFile BPMN { get; set; }
	public string? Link { get; set; }
}
