﻿namespace Persistence.SeedData;

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

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
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

        var defaultTenantId = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00");
        if (!_context.Users.Any())
        {
            _context.Users.Add(new User
            {
                UserName = "ris.shohan@gmail.com",
                Email = "ris.shohan@gmail.com",
                IsActive = true,
                Role = ERoles.MasterAdmin,
                TenantId = defaultTenantId
            });

            await _context.SaveChangesAsync();
        }
    }
}
