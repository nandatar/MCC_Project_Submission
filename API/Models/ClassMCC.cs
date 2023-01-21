using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("class_mcc")]
public class ClassMCC
{
	[Key, Column("id")]
	public int ID { get; set; }
	[Required, Column("name"), MaxLength(20)]
	public string Name { get; set; }

	//relation
	[JsonIgnore]
	public ICollection<Employee>? Employees { get; set; }
}
