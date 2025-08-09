using DataAccessLibrary.Repository;
using System.Text.Json.Serialization;
using TicketingApi.Services;
using TicketingApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProjectRepository>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddSingleton<TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<Validator>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
