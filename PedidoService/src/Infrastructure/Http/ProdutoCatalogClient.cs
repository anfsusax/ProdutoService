using System.Net.Http.Json;
using System.Text.Json;
using System.Net;
using Pedido.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Pedido.Infrastructure.Http;

public class ProdutoCatalogClient : IProdutoCatalogClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProdutoCatalogClient> _logger;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ProdutoCatalogClient(HttpClient httpClient, ILogger<ProdutoCatalogClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ProdutoInfo?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (_httpClient.BaseAddress == null)
            {
                _logger.LogError("BaseAddress do HttpClient não está configurado!");
                throw new InvalidOperationException("BaseAddress do HttpClient não está configurado.");
            }

            var url = $"/produtos/{id}";
            var fullUrl = new Uri(_httpClient.BaseAddress, url);
            
            _logger.LogInformation("Consultando produto {ProdutoId} em {FullUrl} (BaseAddress: {BaseAddress}, Timeout: {Timeout}s)", 
                id, fullUrl, _httpClient.BaseAddress, _httpClient.Timeout.TotalSeconds);

            HttpResponseMessage? response = null;
            try
            {
                response = await _httpClient.GetAsync(url, cancellationToken);
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, 
                    "Falha na requisição HTTP para {FullUrl}. " +
                    "Verifique se o ProdutoService está rodando e acessível. " +
                    "Erro: {Message}. InnerException: {InnerException}", 
                    fullUrl, httpEx.Message, httpEx.InnerException?.Message);
                throw;
            }
            catch (TaskCanceledException canceledEx)
            {
                if (canceledEx.CancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Requisição cancelada para produto {ProdutoId}", id);
                    throw;
                }
                else
                {
                    _logger.LogError(canceledEx, 
                        "Timeout na requisição para {FullUrl}. " +
                        "O ProdutoService pode estar lento ou não está respondendo. " +
                        "Timeout configurado: {Timeout}s", 
                        fullUrl, _httpClient.Timeout.TotalSeconds);
                    throw new HttpRequestException($"Timeout ao consultar produto {id} em {fullUrl}. Verifique se o ProdutoService está rodando.", canceledEx);
                }
            }

            _logger.LogInformation("Resposta recebida: StatusCode={StatusCode}", response.StatusCode);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Produto {ProdutoId} não encontrado", id);
                return null;
            }

            response.EnsureSuccessStatusCode();

            if (!response.Content.Headers.ContentLength.HasValue || response.Content.Headers.ContentLength.Value == 0)
            {
                _logger.LogWarning("Resposta vazia para produto {ProdutoId}", id);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogDebug("Conteúdo recebido: {Content}", content);

            ProdutoInfo? produto = null;
            try
            {
                produto = JsonSerializer.Deserialize<ProdutoInfo>(content, JsonOptions);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Erro ao deserializar resposta do produto {ProdutoId}. Conteúdo: {Content}", id, content);
                throw new HttpRequestException($"Erro ao processar resposta do produto {id}: {jsonEx.Message}", jsonEx);
            }
            
            if (produto == null)
            {
                _logger.LogWarning("Falha ao deserializar produto {ProdutoId} - resultado foi null", id);
            }
            else
            {
                _logger.LogInformation("Produto {ProdutoId} encontrado: {Nome}", id, produto.Nome);
            }

            return produto;
        }
        catch (HttpRequestException)
        {
            // Re-throw - já foi logado acima
            throw;
        }
        catch (TaskCanceledException)
        {
            // Re-throw - já foi logado acima
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao consultar produto {ProdutoId}: {Message}", id, ex.Message);
            throw new HttpRequestException($"Erro inesperado ao consultar produto {id}: {ex.Message}", ex);
        }
    }
}
