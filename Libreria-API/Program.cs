using Libreria_API.Models;
using Libreria_API.Repositories.Implementations;
using Libreria_API.Repositories.Interfaces;
using Libreria_API.Services.Implementations;
using Libreria_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS para desarrollo: acepta cualquier frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()      // permite cualquier origen
                  .AllowAnyHeader()      // permite cualquier header
                  .AllowAnyMethod();     // permite cualquier método (GET, POST, etc)
        });
});

// Add services to the container.
builder.Services.AddDbContext<LibreriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<ILibroService, LibroService>();

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// **Importante:** CORS antes de Authorization
app.UseCors("PermitirFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run();
