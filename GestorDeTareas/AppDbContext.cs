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
    public DbSet<Notificaciones> Notificaciones { get; set; }
    public DbSet<EspaciosDeTrabajo> EspaciosDeTrabajo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarea>()
            .HasDiscriminator<TiposTarea?>("TiposTarea")
            .HasValue<Tarea>(TiposTarea.Simple)
            .HasValue<TareaUrgente>(TiposTarea.Urgente);

        modelBuilder.Entity<Amigos>()
            .HasOne(a => a.Emisor)
            .WithMany()
            .HasForeignKey(a => a.IdEmisor)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Amigos>()
            .HasOne(a => a.Receptor)
            .WithMany()
            .HasForeignKey(a => a.IdReceptor)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notificaciones>()
            .HasOne(a => a.Emisor)
            .WithMany()
            .HasForeignKey(a => a.IdEmisor)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notificaciones>()
            .HasOne(a => a.Receptor)
            .WithMany()
            .HasForeignKey(a => a.IdReceptor)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EspaciosDeTrabajo>()
            .HasMany(e => e.Usuarios)
            .WithMany(u => u.EspaciosDeTrabajo);
    }
}