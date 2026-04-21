using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        //public DbSet<Tarea> Tareas { get; set; }
        //public DbSet<TareasPorUsuario> TareasPorUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=gestorDeTareas;Trusted_Connection=True;");
        }
    }
}
