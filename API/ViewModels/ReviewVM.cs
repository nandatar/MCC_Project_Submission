namespace API.ViewModels;

public class ReviewVM
{
	public int? ProjectID { get; set; }
	public string Time { get; set; }
	public string? Message { get; set; }
	public string? Revision { get; set; }
	public int StatusID { get; set; }
}
