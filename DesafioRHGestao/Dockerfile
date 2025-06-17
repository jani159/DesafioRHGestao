
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar o arquivo de projeto e restaurar as dependências
COPY ["DesafioRHGestao/PedidoCompra.API.csproj", "DesafioRHGestao/"]
RUN dotnet restore "DesafioRHGestao/PedidoCompra.API.csproj"

# Copiar o restante do código-fonte e compilar o projeto
COPY . .

WORKDIR "/src/DesafioRHGestao"
RUN dotnet build "PedidoCompra.API.csproj" -c Release -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PedidoCompra.API.csproj" -c Release -o /app/publish

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PedidoCompra.API.dll"]