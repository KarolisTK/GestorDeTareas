using GestorDeTareas;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Middleware;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("GestorTareas")));

builder.Services.AddScoped<IRepositorio<Tarea>, Repository<Tarea>>();
builder.Services.AddScoped<IRepositorio<TareaUrgente>, Repository<TareaUrgente>>();
builder.Services.AddScoped<IRepositorio<Usuario>, Repository<Usuario>>();
builder.Services.AddScoped<IAmigosRepository, AmigosRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<INotificacionesRepository, NotificacionesRepository>();
builder.Services.AddScoped<IEspaciosDeTrabajoRepository, EspaciosDeTrabajoRepository>();
builder.Services.AddScoped<ISolicitudesRepository, SolicitudesRepository>();
builder.Services.AddScoped<ITareasRepository, TareasRepository>();


builder.Services.AddScoped<TareaService>();
builder.Services.AddScoped<TareaUrgenteService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AmigosService>();
builder.Services.AddScoped<NotificacionesService>();
builder.Services.AddScoped<EspaciosDeTrabajoService>();
builder.Services.AddScoped<SolicitudesService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();
builder.WebHost.UseUrls("http://0.0.0.0:8080");


var app = builder.Build();


app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();