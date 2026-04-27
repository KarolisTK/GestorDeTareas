using GestorDeTareas;
using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Models;
using GestorDeTareas.Services;

var context = new AppDbContext();
var usuarioService = new UsuarioService(new Repository<Usuario>(context));
var tareaService = new TareaService<Tarea>(new Repository<Tarea>(context));
var tareaUrgenteService = new TareaUrgenteService(new Repository<TareaUrgente>(context), new Repository<Tarea>(context));
ProbarTareas();
//PriorizarTarea();
//CrearTareaUrgente();
//QuitarPrioridad();

void ProbarUsuarios()
{
    Console.WriteLine("== Usuarios ==");

    usuarioService.CrearUsuario(new UsuarioDTO
    {
        NombreUsuario = "paco",
        CorreoUsuario = "paco@test.com",
        ContrasenaUsuario = "paco"
    });

    usuarioService.IniciarSesion("paco@test.com", "paco");
    Console.WriteLine($"Sesión iniciada: id={Sesion.IdUsuarioSesionActiva}");

    usuarioService.EditarUsuario(new EditarUsuarioDTO { NombreUsuario = "pacoEditado" });
    Console.WriteLine("Nombre editado");

    usuarioService.EliminarUsuario();
    Console.WriteLine("Usuario eliminado");
}

void ProbarTareas()
{
    Console.WriteLine("== Tareas ==");

    usuarioService.IniciarSesion("paco@test.com", "paco");

    tareaService.CrearTarea(new CrearTareaDTO
    {
        NombreTarea = "tarea de prueba",
        DescripcionTarea = "descripcion",
        FechaCreacionTarea = DateTime.Now,
        EstadosTarea = EstadosTarea.NoIniciada,
        EstaEliminado = false,
        TiposTarea = TiposTarea.Urgente
    });

    tareaService.EditarTarea(4, new EditarTareaDTO { NombreTarea = "tarea editada" });
    Console.WriteLine("Tarea editada");

    tareaService.EliminarTarea(1);
    Console.WriteLine("Tarea eliminada");
}

void PriorizarTarea()
{
    usuarioService.IniciarSesion("paco@test.com", "paco");
    var tareaUrgente = new CrearTareaUrgenteDTO
    {
        FechaLimite = new DateTime(2026, 04, 23, 14, 30, 00),
        TienePrioridad = true
    };
    tareaUrgenteService.PriorizarTarea(6, tareaUrgente);
}

void QuitarPrioridad()
{
    usuarioService.IniciarSesion("paco@test.com", "paco");
    var tareaSimple = new CrearTareaDTO
    {

    };
    tareaUrgenteService.QuitarPrioridadTarea(24, tareaSimple);
}

void CrearTareaUrgente()
{
    usuarioService.IniciarSesion("paco@test.com", "paco");
    tareaUrgenteService.CrearTareaUrgente(new CrearTareaUrgenteDTO
    {
        NombreTarea = "tarea de prueba Creada como tarea Urgente",
        DescripcionTarea = "descripcion",
        FechaCreacionTarea = DateTime.Now,
        EstadosTarea = EstadosTarea.NoIniciada,
        EstaEliminado = false,
        TiposTarea = TiposTarea.Urgente,
        FechaLimite = new DateTime(2026, 04, 23, 14, 30, 00),
        TienePrioridad = true
    });
}