namespace DataModel.DTO;

public class DTO_ProjectInfo
{
    public int Id { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public string? RmsNumber { get; set; }
    public DateTime? ProductionDate { get; set; }
    
    //DTO only
    public int PassPercent { get; set; }
    public int TestCaseNum { get; set; }
    
    List<DTO_Suite>? _suites { get; set; }
}