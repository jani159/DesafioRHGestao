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
    c.SwaggerDoc("v1", new() { Title = "PedidoCompra API", Version = "v1" });
});

var app = builder.Build();

// Configura o middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PedidoCompra API v1"));
}

app.Run();
