using System.ComponentModel.DataAnnotations.Schema;

namespace API.ViewModels;

public class SubmitVM
{
	public string NIK { get; set; }
	public int? ID { get; set; }

	public string ProjectTitle { get; set; }

	public string Description { get; set; }

	public Byte[]? UML { get; set; }

	public Byte[]? BPMN { get; set; }

	public string? Link { get; set; }

	public int CurrentStatus { get; set; }
}
