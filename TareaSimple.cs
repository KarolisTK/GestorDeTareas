using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public class TareaSimple : Tarea
    {
        private TipoTareaSimple _TipoTareaSimple;

        public TipoTareaSimple TipoTareaSimple { get { return _TipoTareaSimple; } set { value = _TipoTareaSimple; }  }
        public TareaSimple(int idTarea, string nombreTarea, string descripcionTarea, DateTime fechaCreacionTarea, EstadoTarea estadoTarea, bool estaEliminado, bool estacompleta, TipoTareaSimple tipoTareaSimple)
        : base(idTarea, nombreTarea, descripcionTarea, fechaCreacionTarea, estadoTarea, estaEliminado, estacompleta) 
        { 
            TipoTareaSimple = tipoTareaSimple;
        }

        public override List<Tarea> ObtenerListadoTareas()
        {
            return ListadoTareasSimples.ObtenerTareasDePrueba().Cast<Tarea>().ToList();
        }

        public override void CrearTarea(string nombreTarea, string descripcionTarea)
        {
            NombreTarea = nombreTarea;
            DescripcionTarea = descripcionTarea;
            FechaCreacionTarea = DateTime.Now;
            EstadoTarea = EstadoTarea.NoIniciada;
            EstaEliminado = false;
            TipoTareaSimple = TipoTareaSimple.Base;
        }

        public override void EditarTarea(int idTarea, string nuevoNombreTarea, string NuevaDescripcionTarea, EstadoTarea NuevoEstadoTarea)
        {
            base.EditarTarea(idTarea, nuevoNombreTarea, NuevaDescripcionTarea, NuevoEstadoTarea);
        }

    }
}
