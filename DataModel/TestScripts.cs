using System.ComponentModel.DataAnnotations;

namespace DataModel;

public class TestScripts : BaseEntity 
{
    [Key]
    public int Id { get; set; }
    
    public required TestCases TestCase { get; set; }
    
    
    //optional
    public string? TestScriptStepNum { get; set; }
    public string? TestScriptStepDescription { get; set; }
    public string? TestScriptExpectedResult { get; set; }
    public string? TestScriptResult { get; set; } // Enum PASS, FAIL, WAIT, ETC.
    public string? Environment { get; set; } //Enum T1, T2, R3, etc
    public int TestScriptCompletedPercent { get; set; } // 100%, 0%
    public string? ReferToTransaction { get; set; } // i.e. TrxId
    
}