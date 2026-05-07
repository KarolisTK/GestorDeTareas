using GestorDeTareas.Enums;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public DbSet<TareaUrgente> TareasUrgentes { get; set; }
    public DbSet<Amigos> Amigos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarea>()
            .HasDiscriminator<TiposTarea?>("TiposTarea")
            .HasValue<Tarea>(TiposTarea.Simple)
            .HasValue<TareaUrgente>(TiposTarea.Urgente);

        modelBuilder.Entity<Amigos>()
            .HasKey(a => new { a.IdUsuario, a.IdUsuario2 });

        modelBuilder.Entity<Amigos>()
            .HasOne(a => a.Usuario)
            .WithMany()
            .HasForeignKey(a => a.IdUsuario)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Amigos>()
            .HasOne(a => a.Usuario2)
            .WithMany()
            .HasForeignKey(a => a.IdUsuario2)
            .OnDelete(DeleteBehavior.Restrict);
    }
}