using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSWEB.Server.Entitites;

[Table("ProductCategories", Schema = "product")]
public class ProductCategory //: IBaseEntity
{
    [Key, Column(Order = 0)]
    public short CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public string Description { get; set; } = string.Empty;
    public required bool IsActive { get; set; }
    [NotMapped]
    public string Status => IsActive ? "Active" : "Inactive";

    public required User CreatedBy { get; set; }
    public DateTime CreatedTime { get; set; }
    public User? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedTime { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}
