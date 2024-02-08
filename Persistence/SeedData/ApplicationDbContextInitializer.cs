using Microsoft.Extensions.Configuration;

namespace Persistence.SeedData;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initialiser.InitializeAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary

        var defaultTenantId = _configuration.GetSection("DefaultTenant:TenantId").Get<string>();
        var tenantId = new Guid(defaultTenantId);

        if (!_context.Users.Any())
        {
            _context.Users.Add(new User
            {
                UserName = "ris.shohan@gmail.com",
                Email = "ris.shohan@gmail.com",
                IsActive = true,
                Role = ERoles.MasterAdmin,
                TenantId = tenantId
            });

            await _context.SaveChangesAsync();
        }
        var userId = _context.Users.Select(x => x.Id).FirstOrDefault();
        var actionDateTime = DateTime.Now;


        if (!_context.ProductCategories.Any())
        {
            _context.ProductCategories.Add(new ProductCategory
            {
                CategoryName = "General",
                CreatedById = userId,
                IsActive = true,
                Description = "It's a general category",
                TenantId = tenantId

            });
            await _context.SaveChangesAsync();
        }
    }
}
