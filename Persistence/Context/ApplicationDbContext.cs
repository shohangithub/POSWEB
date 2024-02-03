using Domain.Entitites;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Persistence.Tenant;

namespace Persistence.Context;

public class ApplicationDbContext : DbContext
{
    private readonly TenantProvider _tenantProvider;
    private readonly Guid _tenantId;
    private readonly string defaultTenantId = "11223344-5566-7788-99AA-BBCCDDEEFF00";
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, TenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
        var tenantId = _tenantProvider.GetTenantId();
        _tenantId = tenantId == Guid.Empty ? new Guid(defaultTenantId) : tenantId;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductUnit> ProductUnits { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //base.OnModelCreating(builder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(x => x.TenantId);
            entity.HasQueryFilter(x => x.TenantId == _tenantId);

            entity.HasMany(x => x.ProductsCreated).WithOne(x => x.CreatedBy).HasForeignKey(x => x.CreatedById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(x => x.ProductsUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey(x => x.LastUpdatedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(x => x.ProductUnitsCreated).WithOne(x => x.CreatedBy).HasForeignKey(x => x.CreatedById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(x => x.ProductUnitsUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey(x => x.LastUpdatedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);


            entity.HasMany(x => x.ProductCategoriesCreated).WithOne(x => x.CreatedBy).HasForeignKey(x => x.CreatedById).IsRequired().OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(x => x.ProductCategoriesUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey(x => x.LastUpdatedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

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
