using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class TestSuites : BaseEntity 
{
    
    //this is excel sheet
    
    [Key]
    public int Id { get; set; }

    public int ProjectId { get; set; }
    public Projects Project { get; set; }
    
    
    //optional
    public int SuiteId { get; set; }
    public string? JiraReference { get; set; } //requirement id --ref to fsd id
    public string? Cr3atReference { get; set; }
    public string? RequirementName { get; set; }
    public string? RequirementDescription { get; set; }
    public string? ChangeDescription { get; set; }
    public virtual ICollection<TestCases> TestCases { get; } = new List<TestCases>();
    //DTO ?
    //number of test cases in this test suite
    // % completamento 
    
    
}