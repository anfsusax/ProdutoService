using Api.Extensions;
using Api.Extensions.Api.Extensions;
using Api.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services
       .AddInfrastructure(builder.Configuration.GetConnectionString("Default"))
       .AddApplicationServices()
       .AddSwaggerDocumentation()
       .AddHealthChecksConfiguration();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
 
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSerilogRequestLogging();


app.Run();


