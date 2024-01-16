namespace Domain.Entitites;

[Table("ProductUnits", Schema = "lookup")]
public class ProductUnit : AuditableEntity<short>
{
    public required string UnitName { get; set; }
    public string Description { get; set; } = string.Empty;
    public required bool IsActive { get; set; }
    [NotMapped]
    public string Status => IsActive ? "Active" : "Inactive";


    public ICollection<Product> Products { get; set; } = [];
}