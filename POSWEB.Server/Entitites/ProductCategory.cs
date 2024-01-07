using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSWEB.Server.Entitites;

[Table("ProductCategories", Schema = "product")]
public class ProductCategory : BaseEntity
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    public required string CategoryName { get; set; }
    public string? Description { get; set; } = string.Empty;
    public required bool IsActive { get; set; }
    [NotMapped]
    public string Status => IsActive ? "Active" : "Inactive";

    
    public ICollection<Product> Products { get; set; } = [];
}

public class ProductCategoryPayload
{
    public required string CategoryName { get; set; }
    public string? Description { get; set; } = string.Empty;
    public required bool IsActive { get; set; }
}