using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("history")]
public class History
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Key, Column("id")]
	public int? ID { get; set; }
	[Column("project_id")]
	public int? ProjectID { get; set; }
	[Column("time")]
	public string Time { get; set; }
	[Column("message")]
	public string? Message { get; set; }
	[Column("revision")]
	public string? Revision { get; set; }
	[Column("status_id")]
	public int? StatusID { get; set; }

	//relasi
	[JsonIgnore]
	[ForeignKey("ProjectID")]
	public Project? Project { get; set; }
	[JsonIgnore]
	[ForeignKey("StatusID")]
	public Status? Status { get; set; }
}
