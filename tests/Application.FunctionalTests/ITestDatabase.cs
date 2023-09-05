using Npgsql;

namespace Blog.Application.FunctionalTests;

public interface ITestDatabase
{
    Task InitialiseAsync();

    NpgsqlConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();

}