using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Blog.Infrastructure.Persistence;
using Npgsql;

namespace Blog.Application.FunctionalTests;

public class PostgresqlTestDatabase : ITestDatabase
{
    private readonly string _connectionString = null!;
    private NpgsqlConnection _connection = null!;

    public PostgresqlTestDatabase()
    {
        var configuration = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json")
       .AddEnvironmentVariables()
       .Build();

        var connectionString = configuration.GetConnectionString("PostgreSqlTest");

        ArgumentNullException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
        _connectionString = connectionString;

    }

    public async Task InitialiseAsync()
    {
        try
        {


            _connection = new NpgsqlConnection(_connectionString);


            NpgsqlConnectionStringBuilder options = new(_connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseNpgsql(_connectionString);

            var context = new ApplicationDbContext(optionsBuilder.Options);

            context.Database.Migrate();

        }
        catch (System.Exception)
        {
            throw;
        }

    }

    public async Task ResetAsync()
    {
        await this.InitialiseAsync();
    }
    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }

    NpgsqlConnection ITestDatabase.GetConnection()
    {
        return _connection;
    }
}