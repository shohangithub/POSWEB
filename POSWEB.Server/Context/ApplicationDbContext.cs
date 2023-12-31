using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Entitites;
using System.Reflection.Metadata;

namespace POSWEB.Server.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.ProductsCreated).WithOne(x => x.CreatedBy).HasForeignKey("ProductsCreated").IsRequired().OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(x => x.ProductsUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey("ProductsUpdated").IsRequired(false).OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.ProductCategoriesCreated).WithOne(x => x.CreatedBy).HasForeignKey("ProductCategoriesCreated").IsRequired().OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(x => x.ProductCategoriesUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey("ProductCategoriesUpdated").IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            });

            //    modelBuilder.Entity<User>().HasMany(e => e.Products).WithOne(x => x.CreatedBy).HasForeignKey("UserId").IsRequired(true);



            //modelBuilder.Entity<AuditEntry>();

            //modelBuilder.Entity<Product>(e =>
            //{
            //    e.Property(b => b.Purchase_Rate).HasColumnType("decimal(10, 2)");
            //    e.Property(b => b.Purchase_Rate).HasColumnType("decimal(10, 2)");
            //});

        }
    }
}
