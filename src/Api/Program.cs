using Api.Middleware;
using Application.Commands.Produtos.Atualizar;
using Application.Commands.Produtos.AtualizarDados;
using Application.Commands.Produtos.CriarProduto;
using Application.Interfaces;
using Application.Queries.Produtos.ObterPorId;
using Application.Queries.Produtos.ObterTodos;
using Infrastructure.EventBus;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Produto.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProdutoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddScoped<IEventPublisher, EventPublisher>();

builder.Services.AddScoped<CriarProdutoHandler>();

builder.Services.AddScoped<ObterTodosProdutosQueryHandler>();
builder.Services.AddScoped<ObterProdutoPorIdQueryHandler>();

builder.Services.AddScoped<AtualizarProdutoHandler>();
builder.Services.AddScoped<AtualizarPrecoHandler>();
builder.Services.AddScoped<AtualizarDadosProdutoHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Produto API",
        Version = "v1.1",
        Description = "API para gerenciamento de produtos."
    });
});

builder.Services.AddOpenApi();


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();


