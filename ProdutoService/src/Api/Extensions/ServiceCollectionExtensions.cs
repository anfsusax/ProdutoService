using Application.Commands.Produtos.Atualizar;
using Application.Commands.Produtos.AtualizarDados;
using Application.Commands.Produtos.CriarProduto;
using Application.Commands.Produtos.Reativar;
using Application.Interfaces;
using Application.Queries.Produtos.ObterPorId;
using Application.Queries.Produtos.ObterTodos;
using Application.Commands.Desativar;
using Infrastructure.EventBus;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Produto.Infrastructure.Repositories;
using Infrastructure.OutBox;

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
            services.AddScoped<DesativarProdutoHandler>();
            services.AddScoped<ReativarProdutoHandler>();

            // Repositórios
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            // EventPublisher
            services.AddScoped<IEventPublisher, EventPublisher>();

            // Outbox
            services.AddScoped<IOutboxWriter, EfOutboxWriter>();

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
 
