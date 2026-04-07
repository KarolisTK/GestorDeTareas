using GestorDeTareas;
using System;
using System.Collections.Generic;

public class ListadoTareas : Tarea
{

    public ListadoTareas(int idTarea, string nombreTarea, string descripcionTarea, DateTime fechaCreacionTarea, EstadoTarea estadoTarea, bool estaEliminado, bool estacompleta)
        :base(idTarea, nombreTarea, descripcionTarea, fechaCreacionTarea, estadoTarea, estaEliminado, estacompleta)
    {
    }
    public static List<ListadoTareas> ObtenerTareasDePrueba() => new()
    {
        new ListadoTareas(1, "Comprar alimentos", "Comprar leche, pan y frutas", DateTime.Today.AddDays(1), EstadoTarea.finalizada, false, false),
        new ListadoTareas(2, "Preparar informe", "Resumen semanal del estado del proyecto", DateTime.Today.AddDays(3), EstadoTarea.NoIniciada, false, false),
        new ListadoTareas(3, "Llamar al cliente", "Confirmar requisitos y fecha de entrega", DateTime.Today, EstadoTarea.pausada, false, false),
        new ListadoTareas(4, "Actualizar dependencias", "Actualizar paquetes NuGet a versiones compatibles", DateTime.Today.AddDays(7), EstadoTarea.finalizada, false, false),
        new ListadoTareas(5, "Revisión de código", "Revisar PR #42 y dejar comentarios", DateTime.Today.AddDays(2), EstadoTarea.abandonada, false, false)
    };
}
