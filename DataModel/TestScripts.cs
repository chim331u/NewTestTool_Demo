using System.ComponentModel.DataAnnotations;
using DataModel.Enum;

namespace DataModel;

public class TestScripts : BaseEntity 
{
    [Key]
    public int Id { get; set; }
    
    public int TestCaseId { get; set; }
    public TestCases TestCase { get; set; }
    
    //optional
    public string? ScriptStepNum { get; set; }
    public string? ScriptStepDescription { get; set; }
    public string? ScriptExpectedResult { get; set; }
    public Results? ScriptResult { get; set; } // Enum PASS, FAIL, WAIT, ETC.
    public Environments? ScriptEnvironment { get; set; } //Enum T1, T2, R3, etc
    public double ScriptCompletedPercent { get; set; } = 0;     // 100%, 0%
    public string? ScriptReferToTransaction { get; set; } // i.e. TrxId
    public string? ScriptRowMessage { get; set; }
    
}