namespace Domain.Entitites;

[Table("Users", Schema = "user")]
[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity<int>
{
    public required string UserName { get; set; }
    [EmailAddress(ErrorMessage="Invalid email address")]
    public required string Email { get; set; }
    public ERoles Role { get; set; }
    public required bool IsActive { get; set; }
    [NotMapped]
    public string Status => IsActive ? "Active" : "Inactive";


    public ICollection<Product> ProductsCreated { get; set; } = [];
    public ICollection<Product> ProductsUpdated { get; set; } = [];

    public ICollection<ProductCategory> ProductCategoriesCreated { get; set; } = [];
    public ICollection<ProductCategory> ProductCategoriesUpdated { get; set; } = [];

    public ICollection<ProductUnit> ProductUnitsCreated { get; set; } = [];
    public ICollection<ProductUnit> ProductUnitsUpdated { get; set; } = [];
}
