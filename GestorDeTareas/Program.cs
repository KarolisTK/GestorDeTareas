using GestorDeTareas;
using GestorDeTareas.DTOs;
using GestorDeTareas.Models;

var context = new AppDbContext();
var usuarioService = new UsuarioService(new Repository<Usuario>(context));
var tareaService = new TareaService(new Repository<Tarea>(context));

ProbarTareas();
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

    tareaService.EditarTarea(1, new EditarTareaDTO { NombreTarea = "tarea editada" });
    Console.WriteLine("Tarea editada");

    tareaService.EliminarTarea(1);
    Console.WriteLine("Tarea eliminada");
}