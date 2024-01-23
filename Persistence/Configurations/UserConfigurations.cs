namespace Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasMany(x => x.ProductsCreated).WithOne(x => x.CreatedBy).HasForeignKey(x => x.CreatedById).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.ProductsUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey(x => x.LastUpdatedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.ProductUnitsCreated).WithOne(x => x.CreatedBy).HasForeignKey(x => x.CreatedById).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.ProductUnitsUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey(x => x.LastUpdatedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);


        builder.HasMany(x => x.ProductCategoriesCreated).WithOne(x => x.CreatedBy).HasForeignKey(x => x.CreatedById).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.ProductCategoriesUpdated).WithOne(x => x.LastUpdatedBy).HasForeignKey(x => x.LastUpdatedById).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

    }
}
