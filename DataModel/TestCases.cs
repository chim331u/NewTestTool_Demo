using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class TestCases : BaseEntity 
{
    //excel row
    [Key]
    public int Id { get; set; }

    public int TestSuiteId { get; set; }
    public TestSuites TestSuite { get; set; }
    
    
    //optional
    public string? TestCaseId { get; set; } // i.e. 1.1, 1.2, etc
    public string? TestCaseName { get; set; }
    public string? TestCaseDescription { get; set; }
    public string? TestCasePrecondition { get; set; }
    public string? ChangeDescription { get; set; }
    
    
    public virtual ICollection<TestScripts> TestScripts { get; } = new List<TestScripts>();
    
    //DTO ?
    //number of test script in this test case
    // % completamento 
}