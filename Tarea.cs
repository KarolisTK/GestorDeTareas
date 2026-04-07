using GestorDeTareas.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public abstract class Tarea : ICompletable, IPriorizable, IAsignable
    {
        private int _IdTarea;
        private string _NombreTarea;
        private string _DescripcionTarea;
        private DateTime _FechaCreacionTarea;
        private EstadoTarea _EstadoTarea;
        private bool _EstaEliminado;
        private bool _EstaCompleta;
        private bool _TienePrioridad;
        private bool _EstaAsignada;

        public int IdTarea
        {
            get
            {
                return _IdTarea;
            }
            set
            {
                _IdTarea = value;
            }
        }

        public string NombreTarea
        {
            get
            {
                return _NombreTarea;
            }
            set
            {
                _NombreTarea = value;
            }
        }

        public string DescripcionTarea
        {
            get
            {
                return _DescripcionTarea;
            }
            set
            {
                _DescripcionTarea = value;
            }
        }

        public DateTime FechaCreacionTarea
        {
            get
            {
                return _FechaCreacionTarea;
            }
            set
            {
                _FechaCreacionTarea = value;
            }
        }

        public EstadoTarea EstadoTarea
        {
            get
            {
                return _EstadoTarea;
            }
            set
            {
                _EstadoTarea = value;
            }
        }

        public bool EstaEliminado
        {
            get
            {
                return _EstaEliminado;
            }
            set
            {
                _EstaEliminado = value;
            }
        }

        public bool EstaCompleta { get { return _EstaCompleta; { } } set { _EstaCompleta = value; } }

        public bool tienePrioridad { get { return _TienePrioridad; } set { _TienePrioridad = value; }  }

        public bool EstaAsignada { get { return _EstaAsignada; } set { _EstaAsignada = value; }  }

        public Tarea(int idTarea, string nombreTarea, string descripcionTarea, DateTime fechaCreacionTarea, EstadoTarea estadoTarea, bool estaEliminado, bool estacompleta)
        {
            _IdTarea = idTarea;
            _NombreTarea = nombreTarea;
            _DescripcionTarea = descripcionTarea;
            _FechaCreacionTarea = fechaCreacionTarea;
            _EstadoTarea = estadoTarea;
            _EstaEliminado = estaEliminado;
            _EstaCompleta= estacompleta;
        }

        public virtual void CrearTarea(string nombreTarea, string descripcionTarea)
        {
            _NombreTarea = nombreTarea;
            _DescripcionTarea = descripcionTarea;
            _FechaCreacionTarea = DateTime.Now;
            _EstadoTarea = EstadoTarea.NoIniciada;
            _EstaEliminado = false;
        }

        public void EditarTarea(int idTarea, string nuevoNombreTarea, string NuevaDescripcionTarea, EstadoTarea NuevoEstadoTarea)
        {
            foreach(var tarea in ListadoTareas.ObtenerTareasDePrueba())
            {
                if (tarea.IdTarea == idTarea)
                {
                    tarea.NombreTarea = nuevoNombreTarea;
                    tarea.DescripcionTarea = NuevaDescripcionTarea;
                    tarea.EstadoTarea = NuevoEstadoTarea;
                }
            }
        }

        public void EliminarTarea(int idTarea, bool estaEliminado)
        {
            bool eliminarTarea = true;
            foreach(var tarea in ListadoTareas.ObtenerTareasDePrueba())
            {
                if(tarea.IdTarea == idTarea && !tarea.EstaEliminado)
                {
                    tarea.EstaEliminado = eliminarTarea;
                }
                throw new ArgumentException("No se ha encontrado la tarea o ya está eliminada");
            }
        }

        public void CambiarEstadoTarea(int idTarea, EstadoTarea nuevoEstadoTarea)
        {
            foreach(var tarea in ListadoTareas.ObtenerTareasDePrueba())
            {
                if (tarea.IdTarea == IdTarea && !tarea.EstaEliminado)
                {
                    if(tarea.EstadoTarea != nuevoEstadoTarea)
                    {
                        tarea.EstadoTarea = nuevoEstadoTarea;
                    }
                    throw new ArgumentException("El estado de la tarea que estás intentando cambiar ya tiene el estado que quieres");
                }
                throw new ArgumentException("No se ha encontrado la tarea o está eliminada");
            }
        }

        public void CompletarTarea(int idtarea)
        {
            foreach (var tarea in ListadoTareas.ObtenerTareasDePrueba())
            {
                if (tarea.IdTarea == idtarea)
                {
                    tarea.EstaCompleta = true;
                    tarea.EstadoTarea = EstadoTarea.finalizada;
                }
            }
        }

        public void PriorizarTarea(int idTarea)
        {
            foreach(var tarea in ListadoTareas.ObtenerTareasDePrueba())
            {
                if(tarea.IdTarea == idTarea)
                {
                    if(tarea.tienePrioridad == false)
                    {
                        tarea.tienePrioridad = true;
                    }
                }
            }
        }

        public void quitarPrioridadTarea(int idTarea)
        {
            foreach (var tarea in ListadoTareas.ObtenerTareasDePrueba())
            {
                if (tarea.IdTarea == idTarea)
                {
                    if (tarea.tienePrioridad == true)
                    {
                        tarea.tienePrioridad = false;
                    }
                }
            }
        }

        public void AsignarTarea(int idTarea, string nombreUsuario)
        {
            throw new NotImplementedException();
        }

        public void quitarAsignacionTarea(int idTarea)
        {
            throw new NotImplementedException();
        }
    }
}
