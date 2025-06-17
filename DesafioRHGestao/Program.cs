using PedidoCompra.Domain.Interfaces;
using PedidoCompra.Infrastructure.Data;
using PedidoCompra.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PedidoCompra.Application.Mappings;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using PedidoCompra.Application.Interfaces;
using PedidoCompra.Application.Services;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateSlimBuilder(args);

// Registrar constraint de regex
builder.Services.Configure<RouteOptions>(options =>
    options.SetParameterPolicy<Microsoft.AspNetCore.Routing.Constraints.RegexInlineRouteConstraint>("regex"));

builder.Services.AddDbContext<PedidoCompraContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra os repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// Registra os serviços
builder.Services.AddScoped<IClienteService, ClienteService>();
//builder.Services.AddScoped<IProdutoService, ProdutoService>();
//builder.Services.AddScoped<IPedidoService, PedidoService>();

//Controllers
builder.Services.AddControllers();

//Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configurações do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Gestor Pedidos API", 
        Version = "v1",
        Description = "API para gestao de pedidos de compra, clientes e produtos."
    });

});

var app = builder.Build();

app.MapControllers();

// Configura o middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestor Pedidos API v1"));
}

// Aplicar as migrations automaticamente ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PedidoCompraContext>();
    dbContext.Database.Migrate(); // Aplica as migrations pendentes
}

app.Run();
