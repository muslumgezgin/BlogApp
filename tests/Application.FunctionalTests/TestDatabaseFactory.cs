using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Application.FunctionalTests;

public static class TestDatabaseFactory
{

    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new PostgresqlTestDatabase();


        await database.InitialiseAsync();

        return database;
    }
    
}