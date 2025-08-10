using DataAccessLibrary.Repository;
using DataAccessLibrary.Repository.Interfaces;
using System.Text.Json.Serialization;
using TicketingApi.Services;
using TicketingApi.Services.Interfaces;
using TicketingApi.Models;
using TicketingApi.Middleware;
using TicketingApi.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TicketingApi;

var builder = WebApplication.CreateBuilder(args);

// Configure connection string
builder.Services.Configure<ConnectionStringOptions>(
    builder.Configuration.GetSection("ConnectionStrings"));

// Register repositories with interfaces
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register services with interfaces
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<Validator>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add caching
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();

// Add health checks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("Database");

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add global exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

// Add health check endpoint
app.MapHealthChecks("/health");

app.Run();
