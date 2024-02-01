using Blog.Infrastructure.Persistence;
using Blog.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();


builder.Services.AddControllers(opt=>{
    opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    opt.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.InitialiseDatabaseAsync();


app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();


public partial class Program { }
