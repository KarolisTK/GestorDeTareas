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
using Resend;
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

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<ITareaUrgenteService, TareaUrgenteService>();
builder.Services.AddScoped<IAmigosService, AmigosService>();
builder.Services.AddScoped<INotificacionesService, NotificacionesService>();
builder.Services.AddScoped<IEspaciosDeTrabajoService, EspaciosDeTrabajoService>();
builder.Services.AddScoped<ISolicitudesService, SolicitudesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200",
                "https://dekarolis.com",
                "https://*.dekarolis.com"
              )
              .SetIsOriginAllowedToAllowWildcardSubdomains()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

builder.Services.AddResend(options =>
{
    options.ApiToken = builder.Configuration["Resend:ApiKey"];
});
builder.Services.AddAuthorization();
builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("PermitirTodo");

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowFrontend");
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