using GestorDeTareas;
using GestorDeTareas.DTOs;

var dto = new CrearTareaDTO
{
    NombreTarea = "tarea",
    DescripcionTarea = "descripcion de tarea Urgente",
    FechaCreacionTarea = System.DateTime.Now,
    EstadosTarea = EstadosTarea.NoIniciada,
    EstaEliminado = false,
    TiposTarea = TiposTarea.Urgente
};
var tareas = new TareaService();
var modificacionDeLDato = new EditarTareaDTO
{
    NombreTarea = "tarea modificada",
    DescripcionTarea = "descripcion de tarea Urgente modificada",
    EstadosTarea = EstadosTarea.NoIniciada,
    EstaEliminado = false,
    TiposTarea = TiposTarea.Simple
};

var eliminarTarea = new EditarTareaDTO
{
    EstaEliminado = true,
};

var modificarTarea = new EditarTareaDTO
{
    EstadosTarea = EstadosTarea.finalizada
};
var modificarTarea2 = new EditarTareaDTO
{
    TiposTarea = TiposTarea.Simple
};
var usuarios = new UsuarioService();
var usuario = new UsuarioDTO
{
    NombreUsuario = "test231",
    CorreoUsuario = "paco@test.com",
    ContrasenaUsuario = "paco",
};
var editarNombreUsuario = new EditarUsuarioDTO
{
    NombreUsuario = "testEditado",
};
var editaremaileUsuario = new EditarUsuarioDTO
{
    CorreoUsuario = "testEditado",
};
var editarContraseñaeUsuario = new EditarUsuarioDTO
{
    ContrasenaUsuario = "testEditado",
};
var eliminarUsuario = new EditarUsuarioDTO
{
    EstaEliminado = true
};
usuarios.CrearUsuario(usuario);
usuarios.IniciarSesion("paco@test.com", "paco");

//tareas.CrearTarea(dto);
////tareas.SacarTareasPorPantalla();
////Console.WriteLine("-------------------------------------------------------");
//tareas.EditarTarea(3, modificacionDeLDato);
////tareas.MostrarTarea(1779872948);
////Console.WriteLine("-------------------------------------------------------");
//tareas.EliminarTarea(3);
////tareas.MostrarTarea(1779872948);
////Console.WriteLine("-------------------------------------------------------");
//tareas.EditarTarea(3, modificarTarea);
////tareas.MostrarTarea(1779872948);
////Console.WriteLine("-------------------------------------------------------");
//tareas.EditarTarea(3, modificarTarea2);
//tareas.MostrarTarea(1779872948);
//usuarios.SacarUsuariosPorPantalla();

usuarios.EditarUsuario(editarNombreUsuario);
usuarios.EliminarUsuario();
usuarios.EditarUsuario(editaremaileUsuario);
usuarios.EditarUsuario(editarContraseñaeUsuario);






//tareas.CrearTarea(dto);

