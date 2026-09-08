using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using TaskFlow.API.Middleware;
using TaskFlow.Application.DTO.Issues;
using TaskFlow.Application.ServiceExtensions;
using TaskFlow.Infrastructure.AppDbContext;
using TaskFlow.Infrastructure.Extensions;


try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration().WriteTo.Console().
                WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();

    builder.Host.UseSerilog();

    #region DbContext

    builder.Services.AddDbContext<TaskFlowDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    #endregion

    // Add services to the container.

    builder.Services.AddInfrastructureServices();

    builder.Services.AddApplicationExtensionService();

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
   await Log.CloseAndFlushAsync();
}
