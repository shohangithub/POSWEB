using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POSWEB.Server.Entitites;

[Table("ProductUnits", Schema = "lookup")]
public class ProductUnit : BaseEntity
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    public required string UnitName { get; set; }
    public string Description { get; set; } = string.Empty;
    public required bool IsActive { get; set; }
    [NotMapped]
    public string Status => IsActive ? "Active" : "Inactive";


    public ICollection<Product> Products { get; set; } = [];
}
