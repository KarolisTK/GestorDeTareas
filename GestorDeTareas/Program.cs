using AutoMapper;
using GestorDeTareas;
using GestorDeTareas.DTOs;

var dto = new CrearTareaDTO
{
    NombreTarea = "tarea",
    DescripcionTarea = "descripcion de tarea Urgente",
    FechaCreacionTarea = System.DateTime.Now,
    EstadoTarea = EstadoTarea.NoIniciada,
    EstaEliminado = false,
    TipoTarea = TipoTarea.Urgente
};
var tareas = new TareaService();
var modificacionDeLDato = new EditarTareaDTO
{
    NombreTarea = "tarea modificada",
    DescripcionTarea = "descripcion de tarea Urgente modificada",
    EstadoTarea = EstadoTarea.NoIniciada,
    EstaEliminado = false,
    TipoTarea = TipoTarea.Simple
};

var eliminarTarea = new EditarTareaDTO
{
    EstaEliminado = true,
};

var modificarTarea = new EditarTareaDTO
{
    EstadoTarea = EstadoTarea.finalizada
};
var modificarTarea2 = new EditarTareaDTO
{
    TipoTarea = TipoTarea.Urgente
};
var usuarios = new UsuarioService();
var usuario = new UsuarioDTO
{
    Name = "test",
    Email = "test@test.com",
    Password = "test",
};
var editarNombreUsuario = new EditarUsuarioDTO
{
    Name = "testEditado",
};
var editaremaileUsuario = new EditarUsuarioDTO
{
    Email = "testEditado",
};
var editarContraseñaeUsuario = new EditarUsuarioDTO
{
    Password = "testEditado",
};
var eliminarUsuario = new EditarUsuarioDTO
{
    EstaEliminado = true
};

//tareas.CrearTarea(dto);
//tareas.SacarTareasPorPantalla();
//Console.WriteLine("-------------------------------------------------------");
//tareas.EditarTarea("0114a580-6984-4065-a388-90e193773177", modificacionDeLDato);
//tareas.MostrarTarea("0114a580-6984-4065-a388-90e193773177");
//Console.WriteLine("-------------------------------------------------------");
//tareas.EliminarTarea("0114a580-6984-4065-a388-90e193773177");
//tareas.MostrarTarea("0114a580-6984-4065-a388-90e193773177");
//Console.WriteLine("-------------------------------------------------------");
//tareas.EditarTarea("0114a580-6984-4065-a388-90e193773177", modificarTarea);
//tareas.MostrarTarea("0114a580-6984-4065-a388-90e193773177");
//Console.WriteLine("-------------------------------------------------------");
//tareas.EditarTarea("0114a580-6984-4065-a388-90e193773177", modificarTarea2);
//tareas.MostrarTarea("0114a580-6984-4065-a388-90e193773177");

//usuarios.CrearUsuario(usuario);
//usuarios.SacarUsuariosPorPantalla();
Console.WriteLine("-------------------------------------------------------");
usuarios.EditarUsuario("e7c3cc78-7735-4162-a73a-e60badcde9a1", editarNombreUsuario);
usuarios.MostrarTarea("e7c3cc78-7735-4162-a73a-e60badcde9a1");
Console.WriteLine("-------------------------------------------------------");
usuarios.EliminarUsuario("e7c3cc78-7735-4162-a73a-e60badcde9a1");
usuarios.MostrarTarea("e7c3cc78-7735-4162-a73a-e60badcde9a1");
Console.WriteLine("-------------------------------------------------------");
usuarios.EditarUsuario("e7c3cc78-7735-4162-a73a-e60badcde9a1", editaremaileUsuario);
usuarios.MostrarTarea("e7c3cc78-7735-4162-a73a-e60badcde9a1");
Console.WriteLine("-------------------------------------------------------");
usuarios.EditarUsuario("e7c3cc78-7735-4162-a73a-e60badcde9a1", editarContraseñaeUsuario);
usuarios.MostrarTarea("e7c3cc78-7735-4162-a73a-e60badcde9a1");




