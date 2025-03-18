using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class TestCases : BaseEntity 
{
    [Key]
    public int Id { get; set; }
    
    public required TestSuites TestSuite { get; set; }
    
    
    //optional
    public string? TestCaseId { get; set; } // i.e. 1.1, 1.2, etc
    public string? TestCaseName { get; set; }
    public string? TestCaseDescription { get; set; }
    public string? TestCasePrecondition { get; set; }
    public string? ChangeDescription { get; set; }
    
    
    //Todo:
    //number of test script in this test case
    // % completamento 
}