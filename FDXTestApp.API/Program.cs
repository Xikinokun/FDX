using FDXTestApp.Application.Configuration;
using FDXTestApp.Infrastructure.Configuration;
using FDXTestApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Host
    .AddApplication()
    .AddInfrastructure();


builder.Host.UseSerilog((context, config) =>
    config.WriteTo.Console());

var app = builder.Build();

using (var scope =
  app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<Context>())
    context!.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
