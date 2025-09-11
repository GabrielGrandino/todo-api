using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using TodoList.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

//DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TodoList.Api",
        Version = "v1",
        Description = "API Para gerenciar tarefas"
    });
});

//Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()           // nível mínimo
    .Enrich.FromLogContext()
    .WriteTo.Console(                      // logs no console
        outputTemplate: "[{Timestamp:HH:mm:ss}][{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(                         // logs em arquivo
        path: "logs/log-.txt",             // o "-" indica rotação diária
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:HH:mm:ss}][{Level:u3}] {Message:lj}{NewLine}{Exception}",
        retainedFileCountLimit: 30)        // mantém 30 dias
    .CreateLogger();

builder.Host.UseSerilog(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoList API V1");
    });
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
