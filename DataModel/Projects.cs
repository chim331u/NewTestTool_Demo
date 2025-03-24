using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class Projects : BaseEntity 
{
    

    //required
    [Key]
    public int Id { get; set; }
    public required string ProjectName { get; set; }

    //optional
    public string? ProjectCode { get; set; }
    public string? RmsNumber { get; set; }
    public DateTime? ProductionDate { get; set; }
    

    
    public virtual ICollection<TestSuites> TestSuites { get; } = new List<TestSuites>();
    
    //DTO only
    public int PassPercent { get; set; }
    public int TestCaseNum { get; set; }
    
    
}