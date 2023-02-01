using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("project")]
public class Project
{
	[Key, Column("id")]
	public int? ID { get; set; }
	[Required, Column("project_title"), MaxLength(50)]
	public string ProjectTitle { get; set; }
	[Required, Column("description")]
	public string Description { get; set; }
	[Column("uml")]
	public Byte[]? UML { get; set; }
	[Column("bpmn")]
	public Byte[]? BPMN { get; set; }
	[Column("link")]
	public string? Link { get; set; }
	[Column("current_status")]
	public int CurrentStatus { get; set; }
	[Column("score")]
	public int? Score { get; set; }

	//relasi
	[JsonIgnore]
	public ICollection<Participant>? Participants { get; set; }
	[JsonIgnore]
	public ICollection<History>? Histories { get; set; }
	[JsonIgnore]
	[ForeignKey("CurrentStatus")]
	public Status? Status { get; set; }
}
