using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("status")]
public class Status
{
	[Key, Column("id")]
	public int? ID { get; set; }
	[Column("name"), MaxLength(50)]
	public string Name { get; set; }

	//relasi
	[JsonIgnore]
	public ICollection<History>? Histories { get; set; }
	[JsonIgnore]
	public ICollection<Project>? Projects { get; set; }
}


