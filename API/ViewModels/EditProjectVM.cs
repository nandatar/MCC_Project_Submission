namespace API.ViewModels;

public class EditProjectVM
{
    
    public int? ID { get; set; }
    
    public string ProjectTitle { get; set; }
    
    public string Description { get; set; }
   
    public IFormFile? UML { get; set; }
   
    public IFormFile? BPMN { get; set; }
    
    public string? Link { get; set; }
    
    public int CurrentStatus { get; set; }
}
