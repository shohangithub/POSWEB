namespace Domain.Entitites;

public abstract class AuditableEntity<T> : BaseEntity<T>
{
    public int CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public int? LastUpdatedById { get; set; }
    public User? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedTime { get; set; }
}
