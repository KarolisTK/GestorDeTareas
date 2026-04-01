using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public enum EstadoTarea
    {
        NoComenzado,
        Comenzado,
        Pausado,
        Finalizado
    }
    public class ClaseTarea
    {
        private static int _contadorId = 0;
        private int _IdTarea { get; set; }
        private string _NombreTarea { get; set; }
        private string _DescripcionTarea { get; set; }
        private DateTime _FechaCreacionTarea { get; set; }
        private EstadoTarea _EstadoTarea { get; set; }
        private bool _EstaEliminado { get; set; }

        public ClaseTarea (int idTarea, string nombreTarea, string descripcionTarea, DateTime fechaCreacionTarea, EstadoTarea estadoTarea, bool estaEliminado)
        {
            _IdTarea = idTarea;
            _NombreTarea = nombreTarea;
            _DescripcionTarea = descripcionTarea;
            _FechaCreacionTarea = fechaCreacionTarea;
            _EstadoTarea = estadoTarea; 
            _EstaEliminado = estaEliminado; 
        }

        public void CrearTarea( string nombreTarea, string descripcionTarea)
        {
            _IdTarea = _contadorId++;
            _NombreTarea = nombreTarea;
            _DescripcionTarea = descripcionTarea;
            _FechaCreacionTarea = DateTime.Now;
            _EstadoTarea = EstadoTarea.NoComenzado;
            _EstaEliminado = false;
        }

        public void EditarTarea(int idTarea, string nuevoNombreTarea, string NuevaDescripcionTarea, EstadoTarea NuevoEstadoTarea)
        {
            if(idTarea == _IdTarea)
            {
                _NombreTarea = nuevoNombreTarea;
                _DescripcionTarea = NuevaDescripcionTarea;
                _EstadoTarea = NuevoEstadoTarea;
                _EstaEliminado = false;
            }
        }

        public void EliminarTarea(int idTarea, bool estaEliminado)
        {
            if (idTarea == _IdTarea)
            {
                _EstaEliminado = estaEliminado;
            }
        }

        public void CambiarEstadoTarea(int idTarea, EstadoTarea nuevoEstadoTarea) 
        {
            if (idTarea == _IdTarea)
            {
                _EstadoTarea = nuevoEstadoTarea;
            }
        }

    }
}
