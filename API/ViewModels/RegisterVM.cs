using System.Reflection;

namespace API.ViewModels;

public class RegisterVM
{
	public string NIK { get; set; }
	public int ClassID { get; set; }
	public string Batch { get; set; }
	public string Email { get; set; }
	public string Fullname { get; set; }
	public string Position { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	
}
