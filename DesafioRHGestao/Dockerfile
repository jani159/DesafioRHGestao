# Esses ARGs permitem a troca da base usada para criar a imagem final durante a depuração do VS
#ARG LAUNCHING_FROM_VS
# Isso define a imagem base definitiva, mas somente se LAUNCHING_FROM_VS tiver sido definido
#ARG FINAL_BASE_IMAGE=${LAUNCHING_FROM_VS:+aotdebug}

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Instalar dependências clang/zlib1g-dev para publicação no nativo
#RUN apt-get update \
#    && apt-get install -y --no-install-recommends \
#    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DesafioRHGestao/PedidoCompra.API.csproj", "DesafioRHGestao/"]
RUN dotnet restore "DesafioRHGestao/PedidoCompra.API.csproj"
COPY . .
WORKDIR "/src/DesafioRHGestao"
RUN dotnet build "PedidoCompra.API.csproj" -c Release -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PedidoCompra.API.csproj" -c Release -o /app/publish

# Esta fase é usada como base para a fase final ao iniciar no VS para dar suporte à depuração no modo normal (Padrão ao não usar a configuração de Depuração)
#FROM base AS aotdebug
#USER root
# Instalar o GDB para dar suporte à depuração nativa
#RUN apt-get update \
#    && apt-get install -y --no-install-recommends \
#    gdb
#USER app

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PedidoCompra.API.dll"]