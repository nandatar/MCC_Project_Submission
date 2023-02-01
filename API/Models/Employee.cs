using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;


[Table("employee")]
public class Employee
{
	[Key, Column("nik", TypeName = "nchar(5)")]
	public string NIK { get; set; }
	[Required, Column("name"), MaxLength(50)]
	public string? Name { get; set; }
	[Required, Column("email"), MaxLength(50)]
	public string? Email { get; set; }
	[Required, Column("position"), MaxLength(50)]
	public string? Position { get; set; }
	[Column("class_id")]
	public int? ClassID { get; set; }


	//relation
	[JsonIgnore]
	[ForeignKey("ClassID")]
	public ClassMCC? ClassMCC { get; set; }
	[JsonIgnore]
	[ForeignKey("NIK")]
	public Account? Account { get; set; }
	[JsonIgnore]
	public Participant? Participant { get; set; }

}