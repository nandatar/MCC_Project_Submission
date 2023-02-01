using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;


[Table("participant")]
public class Participant
{
	[Key, Column("nik", TypeName = "nchar(5)")]
	public string NIK { get; set; }
	[Required, Column("batch"), MaxLength(10)]
	public string Batch { get; set; }
	[Required, Column("status_mcc")]
	public Status_MCC Status_MCC { get; set; }
	[Column("project_id")]
	public int? ProjectID { get; set; }


	//relation
	[JsonIgnore]
	[ForeignKey("NIK")]
	public Employee? Employee { get; set; }
	[JsonIgnore]
	[ForeignKey("ProjectID")]
	public Project? Project { get; set; }


}


public enum Status_MCC
{
	Graduate,
	Participant
}