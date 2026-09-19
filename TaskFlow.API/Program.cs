using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using TaskFlow.API.Middleware;
using TaskFlow.Application.DTO.Issues;
using TaskFlow.Application.ServiceExtensions;
using TaskFlow.Infrastructure.AppDbContext;
using TaskFlow.Infrastructure.Extensions;
using TaskFlow.Infrastructure.Identity;


try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration().WriteTo.Console().
                WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();

    builder.Host.UseSerilog();

    #region DbContext

    builder.Services.AddDbContext<TaskFlowDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;

       options.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<TaskFlowDbContext>().AddDefaultTokenProviders();
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
catch(Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
   await Log.CloseAndFlushAsync();
}
