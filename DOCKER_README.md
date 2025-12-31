# Docker Compose - Microservices

Este projeto está configurado para rodar todos os serviços automaticamente usando Docker Compose.

## Serviços Incluídos

- **PedidoService**: API de Pedidos (porta 5154)
- **ProdutoService**: API de Produtos (porta 5140)
- **pedido-postgres**: Banco de dados PostgreSQL para PedidoService (porta 5433)
- **produto-postgres**: Banco de dados PostgreSQL para ProdutoService (porta 5432)

## Como Usar

### 1. Iniciar todos os serviços

Na raiz do projeto, execute:

```bash
docker-compose up -d
```

O parâmetro `-d` inicia os serviços em modo background (detached).

### 2. Ver logs dos serviços

```bash
# Ver logs de todos os serviços
docker-compose logs -f

# Ver logs de um serviço específico
docker-compose logs -f pedido-service
docker-compose logs -f produto-service
```

### 3. Parar os serviços

```bash
docker-compose down
```

### 4. Parar e remover volumes (limpar dados)

```bash
docker-compose down -v
```

### 5. Reconstruir as imagens

Se você fez alterações no código e precisa reconstruir:

```bash
docker-compose up -d --build
```

## Endpoints Disponíveis

Após iniciar os serviços:

- **ProdutoService**: http://localhost:5140
- **PedidoService**: http://localhost:5154
- **Swagger ProdutoService**: http://localhost:5140/swagger
- **Swagger PedidoService**: http://localhost:5154/swagger
- **Health Check ProdutoService**: http://localhost:5140/health
- **Health Check PedidoService**: http://localhost:5154/health

## Estrutura de Rede

Todos os serviços estão na mesma rede Docker (`microservices-network`), permitindo comunicação entre eles usando os nomes dos serviços:

- PedidoService pode acessar ProdutoService via: `http://produto-service:80`
- Ambos os serviços acessam seus respectivos bancos PostgreSQL usando os nomes: `pedido-postgres` e `produto-postgres`

## Variáveis de Ambiente

As variáveis de ambiente são configuradas no `docker-compose.yml`. As connection strings são ajustadas automaticamente para usar os nomes dos serviços Docker em vez de `localhost`.

## Nota sobre .NET 10.0

O ProdutoService está configurado para usar .NET 10.0. Se essa versão não estiver disponível nas imagens oficiais da Microsoft, você pode precisar ajustar o `ProdutoService/Dockerfile` para usar uma versão disponível (por exemplo, `9.0` ou `8.0`).

## Troubleshooting

### Verificar status dos containers

```bash
docker-compose ps
```

### Verificar se os bancos estão prontos

```bash
docker-compose exec pedido-postgres pg_isready -U postgres
docker-compose exec produto-postgres pg_isready -U postgres
```

### Acessar o banco de dados

```bash
# PedidoService PostgreSQL
docker-compose exec pedido-postgres psql -U postgres -d pedido_db

# ProdutoService PostgreSQL
docker-compose exec produto-postgres psql -U postgres -d produto_db
```

