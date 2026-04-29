using GestorDeTareas;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestorTareas")));

builder.Services.AddScoped<IRepositorio<Tarea>, Repository<Tarea>>();
builder.Services.AddScoped<IRepositorio<TareaUrgente>, Repository<TareaUrgente>>();
builder.Services.AddScoped<IRepositorio<Usuario>, Repository<Usuario>>();

builder.Services.AddScoped<TareaService>();
builder.Services.AddScoped<TareaUrgenteService>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();