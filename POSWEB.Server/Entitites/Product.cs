using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSWEB.Server.Entitites
{

    [Table("Products", Schema = "product")]
    public class Product : IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public required uint ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductCode
        {
            get { return ProductCode; }
            set
            {
                var range = ProductId / 10;

                if (range == 0)
                    ProductCode = $"P-0000{ProductId}";//P-00099
                else if (range <= 9)
                    ProductCode = $"P-000{ProductId}";//P-00099
                else if (range <= 99)
                    ProductCode = $"P-00{ProductId}"; //P-00999
                else if (range <= 999)
                    ProductCode = $"P-0{ProductId}"; //P-09999
                else
                    ProductCode = $"P-{ProductId}"; //P-99999
            }
        }
        public string CustomBarcode { get; set; } = string.Empty;
        public required ProductCategory ProductCategory { get; set; }
        public required ProductUnit ProductUnit { get; set; }
        public required bool IsRawMaterial { get; set; }
        public required bool IsFinishedGoods { get; set; }
        public uint? ReOrederLevel { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PurchaseRate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? SellingRate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? WholesalePrice { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal? VatPercent { get; set; }
        public required bool IsActive { get; set; } = true;
        [NotMapped]
        public string Status => IsActive ? "Active" : "Inactive";

        public required User CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public User? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
    }


}
