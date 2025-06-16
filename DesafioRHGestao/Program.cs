using GestorPedidos.Domain.Interfaces;
using GestorPedidos.Infrastructure.Data;
using GestorPedidos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<GestorPedidoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra os repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();


var app = builder.Build();

app.Run();
