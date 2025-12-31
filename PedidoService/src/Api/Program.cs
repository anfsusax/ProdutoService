using Pedido.Api.Extensions;
using Pedido.Api.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Connection string 'Default' not found.");

builder.Services
       .AddInfrastructure(connectionString)
       .AddApplicationServices()
       .AddSwaggerDocumentation()
       .AddHealthChecksConfiguration();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Apenas redirecionar HTTPS se a URL contiver https
var urls = builder.Configuration["ASPNETCORE_URLS"] ?? "";
if (urls.Contains("https"))
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseSerilogRequestLogging();

app.Run();
