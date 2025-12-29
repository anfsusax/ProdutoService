using Application.Commands.Produtos.Atualizar;
using Application.Commands.Produtos.AtualizarDados;
using Application.Commands.Produtos.CriarProduto;
using Application.Interfaces;
using Application.Queries.Produtos.ObterPorId;
using Application.Queries.Produtos.ObterTodos;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Produto.Infrastructure.Repositories;

namespace Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Handlers
            services.AddScoped<CriarProdutoHandler>();
            services.AddScoped<AtualizarProdutoHandler>();
            services.AddScoped<AtualizarPrecoHandler>();
            services.AddScoped<AtualizarDadosProdutoHandler>();
            services.AddScoped<ObterProdutoPorIdQueryHandler>();
            services.AddScoped<ObterTodosProdutosQueryHandler>();

            // Repositórios
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            // EventPublisher
            services.AddScoped<IEventPublisher, EventPublisher>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProdutoDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }
    }
}
 
