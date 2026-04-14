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

//tareas.CrearTarea(dto);
//tareas.SacarTareasPorPantalla();
Console.WriteLine("-------------------------------------------------------");
tareas.EditarTarea("69696a8f-e9d6-41ae-894c-1ccb1d2e3c10", modificacionDeLDato);
tareas.SacarSoloUnaTareaPorPantalla("69696a8f-e9d6-41ae-894c-1ccb1d2e3c10");
Console.WriteLine("-------------------------------------------------------");
tareas.EliminarTarea("69696a8f-e9d6-41ae-894c-1ccb1d2e3c10", eliminarTarea);
tareas.SacarSoloUnaTareaPorPantalla("69696a8f-e9d6-41ae-894c-1ccb1d2e3c10");
Console.WriteLine("-------------------------------------------------------");



