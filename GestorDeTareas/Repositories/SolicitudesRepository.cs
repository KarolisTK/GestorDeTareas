using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Repositories
{
    public class SolicitudesRepository : Repository<Solicitudes>, ISolicitudesRepository
    {
        public SolicitudesRepository(AppDbContext context) : base(context) { }

        public async Task<List<SolicitudesDTO>> ObtenerSolicitudesPendientes(int id, TiposSolicitudes tiposSolicitudes)
        {
            return await _context.Solicitudes
                .Where(a => a.TiposEstado == TipoEstadoSolicitud.Pendiente && a.IdReceptor == id && a.TiposEstado == TipoEstadoSolicitud.Pendiente)
                .Select(s => new SolicitudesDTO
                {
                    IdSolicitud = s.IdSolicitud,
                    IdSolicitante = s.IdEmisor,
                    IdSolicitado = s.IdReceptor,
                    NombreSolicitante = s.Emisor.NombreUsuario,
                    FechaSolicitud = s.FechaSolicitud,
                    Estado = s.TiposEstado,
                    TiposSolicitudes = s.TiposSolicitudes
                    
                })
                .ToListAsync();
        }
    }
}
