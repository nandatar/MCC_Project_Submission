

namespace API.ViewModels;

public class SubmitProjectVM
{
	public string Title { get; set; }
	public string Description { get; set; }
	public Byte[] UML { get; set; }
	public Byte[] BPMN { get; set; }
	public string Link { get; set; }
}
