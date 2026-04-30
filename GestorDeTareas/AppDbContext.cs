using GestorDeTareas.Enums;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public DbSet<TareaUrgente> TareasUrgentes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarea>()
            .HasDiscriminator<TiposTarea?>("TiposTarea")
            .HasValue<Tarea>(TiposTarea.Simple)
            .HasValue<TareaUrgente>(TiposTarea.Urgente);
    }
}