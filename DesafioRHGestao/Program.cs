using PedidoCompra.Domain.Interfaces;
using PedidoCompra.Infrastructure.Data;
using PedidoCompra.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PedidoCompra.Application.Mappings;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<PedidoCompraContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra os repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.Run();
