using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.ServiceExtensions;
using TaskFlow.Infrastructure.AppDbContext;
using TaskFlow.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region DbContext

builder.Services.AddDbContext<TaskFlowDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

// Add services to the container.

builder.Services.AddInfrastructureServices();

builder.Services.AddApplicationExtensionService();

builder.Services.AddControllers();
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

