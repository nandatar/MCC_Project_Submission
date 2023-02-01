using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using API.Models;

namespace API.ViewModels;

public class MProjectVM
{
    
    public int? ID { get; set; }
    public string ProjectTitle { get; set; }
    public string Description { get; set; }
    public Byte[]? UML { get; set; }
    public Byte[]? BPMN { get; set; }
    public string? Link { get; set; }
    public string StatusName { get; set; }
    public string NIK { get; set; }
    public string Batch { get; set; }
    public Status_MCC Status_MCC { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Position { get; set; }
    public string ClassName { get; set; }
}
