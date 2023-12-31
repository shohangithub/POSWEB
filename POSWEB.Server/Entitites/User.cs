using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POSWEB.Server.Entitites
{
    [Table("Users", Schema = "user")]
    public class User
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string UserName { get; set; }
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
}
