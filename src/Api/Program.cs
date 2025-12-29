using Api.Extensions;
using Api.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services
       .AddInfrastructure(builder.Configuration.GetConnectionString("Default"))
       .AddApplicationServices()
       .AddSwaggerDocumentation();
  
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



app.Run();


