namespace DataModel.DTO;

public class DTO_Suite
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    
    public int SuiteId { get; set; }
    public string? JiraReference { get; set; } //requirement id --ref to fsd id
    public string? Cr3atReference { get; set; }
    public string? RequirementName { get; set; }
    public string? RequirementDescription { get; set; }
    public string? ChangeDescription { get; set; }
    
    public List<DTO_Cases> TestCases { get; set; }
    
    //DTO ?
    //number of test cases in this test suite
    // % completamento 
}