namespace DataModel;

public abstract class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Note { get; set; }
    
}