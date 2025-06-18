# DesafioRHGestao

Sistema de Gestão de Pedidos para o desafio RHGestao.

## Descrição

API para cadastro e controle de clientes, produtos e pedidos de compra, utilizando ASP.NET Core, Entity Framework Core e PostgreSQL.

## Funcionalidades
- Cadastro de clientes
- Cadastro de produtos
- Cadastro e consulta de pedidos de compra
- Validação de regras de negócio no domínio
- Logs estruturados com Serilog
- Documentação automática com Swagger

## Tecnologias Utilizadas
- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- Docker e Docker Compose
- Serilog
- AutoMapper
- xUnit (testes unitários)

## Como rodar o projeto via Docker

### Pré-requisitos
- [Docker](https://www.docker.com/) instalado
- [Docker Compose](https://docs.docker.com/compose/) instalado

### Passos para rodar

1. **Clone o repositório:**
   ```bash
   cd DesafioRHGestao
   ```

2. **Suba os containers:**
   ```bash
   docker-compose up --build
   ```
   Isso irá:
   - Subir um container PostgreSQL já configurado
   - Subir a API ASP.NET Core na porta 8080

3. **Acesse a API:**
   - Swagger: [http://localhost:8080/swagger](http://localhost:8080/swagger)
   - A API estará disponível em [http://localhost:8080](http://localhost:8080)

4. **Parar os containers:**
   ```bash
   docker-compose down
   ```

## Variáveis de ambiente importantes
- `ASPNETCORE_ENVIRONMENT=Development`
- `ConnectionStrings__DefaultConnection=Host=postgres;Port=5432;Database=ordercontroldb;Username=postgres;Password=a1b2c3d4e5`

Essas variáveis já estão configuradas no `docker-compose.yml`.

## Estrutura dos principais serviços
- **Banco de dados:** PostgreSQL (porta 5432)
- **API:** ASP.NET Core (porta 8080)

## Executando os testes unitários

1. **Rode os testes:**
   ```bash
   dotnet test PedidoCompra.Tests/PedidoCompra.Tests.csproj
   ```

## Observações
- As migrations do banco são aplicadas automaticamente ao iniciar a aplicação.
- Os logs são gravados no console e no arquivo `logs/gestor_pedidos.log`.

---