using Blog.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            _logger.LogInformation(message: "Initialising database...");

            if (_context.Database.IsNpgsql())
            {
                _logger.LogInformation(message: "PostgreSQL detected. Ensuring database exists...");
                await _context.Database.MigrateAsync();
            }
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

        if(!_context.Authors.Any())
        {
            _context.Authors.Add(new Author { Name = "J.R.R",SurName = "Tolkien"});
            _context.Authors.Add(new Author { Name = "William",SurName = "Shakespeare"});
            _context.Authors.Add(new Author { Name = "J.K",SurName = "Rowling"});
            _context.Authors.Add(new Author { Name = "Stephen",SurName = "King"});
            _context.Authors.Add(new Author { Name = "George",SurName = "Orwell"});
            _context.Authors.Add(new Author { Name = "Agatha",SurName = "Christie"});
            _context.Authors.Add(new Author { Name = "Dan",SurName = "Brown"});
            _context.Authors.Add(new Author { Name = "Leo",SurName = "Tolstoy"});
            _context.Authors.Add(new Author { Name = "Mark",SurName = "Twain"});
            _context.Authors.Add(new Author { Name = "Oscar",SurName = "Wilde"});
            _context.Authors.Add(new Author { Name = "Jane",SurName = "Austen"});
        }

        await _context.SaveChangesAsync();

    }
}


public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}
