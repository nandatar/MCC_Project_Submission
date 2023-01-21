using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;


[Table("account")]
public class Account
{
	[Key, Column("nik", TypeName = "nchar(5)")]
	public string NIK { get; set; }
	[Required, Column("username"), MaxLength(20)]
	public string Username { get; set; }
	[Required, Column("password")]
	public string Password { get; set; }
	[Required, Column("role")]
	public Role Role { get; set; }
	[Required, Column("is_valid")]
	public bool IsValid { get; set; }


	//relation
	[JsonIgnore]
	public Employee? Employee { get; set; }

}

public enum Role
{
	Trainer,
	Participant
}