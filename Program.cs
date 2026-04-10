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

var Tareas = new TareaService();

Tareas.CrearTarea(dto);

var modificacionDeLDato = new TareaDTO
{
    NombreTarea = "tarea modificada",
    DescripcionTarea = "descripcion de tarea Urgente modificada",
    FechaCreacionTarea = System.DateTime.Now,
    EstadoTarea = EstadoTarea.NoIniciada,
    EstaEliminado = false,
    TipoTarea = TipoTarea.Urgente
};



Tareas.SacarTareasPorPantalla();

Tareas.EditarTarea(1, modificacionDeLDato);

Tareas.SacarTareasPorPantalla();


