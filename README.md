# Clean Architecture Blog Solution

## Getting Started

Launch the app by docker:

```
docker-compose build
docker-compose up
```

Launch the app by terminal:

```bash
cd src/WebAPI
dotnet run
```

## Database

The template is configured to use POSTGRESQL Server by default.

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/WebAPI`
* `--output-dir Data/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\WebAPI --output-dir Data\Migrations`

## Technologies

* [Entity Framework Core ](https://docs.microsoft.com/en-us/ef/core/)7
* [.NET 7](https://dotnet.microsoft.com/en-us/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/)
