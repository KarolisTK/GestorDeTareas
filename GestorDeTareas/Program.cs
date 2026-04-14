using AutoMapper;
using GestorDeTareas;
using GestorDeTareas.DTOs;

var dto = new TareaDTO
{
    NombreTarea = "tarea",
    DescripcionTarea = "descripcion de tarea Urgente",
    FechaCreacionTarea = System.DateTime.Now,
    EstadoTarea = EstadoTarea.NoIniciada,
    EstaEliminado = false,
    TipoTarea = TipoTarea.Urgente
};
var tareas = new TareaService();
var modificacionDeLDato = new TareaDTO
{
    NombreTarea = "tarea modificada",
    DescripcionTarea = "descripcion de tarea Urgente modificada",
    FechaCreacionTarea = System.DateTime.Now,
    EstadoTarea = EstadoTarea.NoIniciada,
    EstaEliminado = false,
    TipoTarea = TipoTarea.Urgente
};

var eliminarTarea = new TareaDTO
{
    EstaEliminado = true,
};

var modificarTarea = new TareaDTO
{
    EstadoTarea = EstadoTarea.iniciada
};
var modificarTarea2 = new TareaDTO
{
    TipoTarea = TipoTarea.Urgente
};

//tareas.CrearTarea(dto);
//tareas.SacarTareasPorPantalla();
//Console.WriteLine("-------------------------------------------------------");
//tareas.EditarTarea("eb5b8439-59be-4a12-b8dc-801b7bde4530", modificacionDeLDato);
//tareas.SacarSoloUnaTareaPorPantalla("eb5b8439-59be-4a12-b8dc-801b7bde4530");
//Console.WriteLine("-------------------------------------------------------");
//tareas.EliminarTarea("eb5b8439-59be-4a12-b8dc-801b7bde4530", eliminarTarea);
//tareas.SacarSoloUnaTareaPorPantalla("eb5b8439-59be-4a12-b8dc-801b7bde4530");
//Console.WriteLine("-------------------------------------------------------");
//tareas.CambiarEstadoTarea("eb5b8439-59be-4a12-b8dc-801b7bde4530", modificarTarea);
tareas.MostrarTarea("eb5b8439-59be-4a12-b8dc-801b7bde4530");
Console.WriteLine("-------------------------------------------------------");
tareas.CambiarTipoTarea("eb5b8439-59be-4a12-b8dc-801b7bde4530", modificarTarea2);
tareas.MostrarTarea("eb5b8439-59be-4a12-b8dc-801b7bde4530");




