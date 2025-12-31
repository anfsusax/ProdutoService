FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos de projeto
COPY ["src/Api/Pedido.Api.csproj", "src/Api/"]
COPY ["src/Application/Pedido.Application.csproj", "src/Application/"]
COPY ["src/Domain/Pedido.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Pedido.Infrastructure.csproj", "src/Infrastructure/"]

# Restaurar dependências
RUN dotnet restore "src/Api/Pedido.Api.csproj"

# Copiar todo o código fonte
COPY . .

# Build da aplicação
WORKDIR "/src/src/Api"
RUN dotnet build "Pedido.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pedido.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pedido.Api.dll"]

