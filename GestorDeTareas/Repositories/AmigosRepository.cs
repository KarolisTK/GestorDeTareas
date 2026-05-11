using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Repositories
{
    public class AmigosRepository : Repository<Amigos>, IAmigosRepository
    {
        public AmigosRepository(AppDbContext context) : base(context) { }

        public async Task<List<SolicitudAmistadDto>> ObtenerSolicitudesPendientes(int id)
        {
            return await _context.Amigos
                .Include(a => a.IdEmisor) 
                .Where(a => a.TiposEstado == TiposEstadoAmistad.Pendiente && a.IdReceptor == id)
                .Select(a => new SolicitudAmistadDto
                {
                    IdSolicitud = a.IdAmigos,
                    IdSolicitante = a.IdEmisor,
                    IdSolicitado = a.IdReceptor,
                    NombreSolicitante = a.Emisor.NombreUsuario,
                    FechaSolicitud = a.FechaInicioAmistad,
                    Estado = a.TiposEstado
                })
                .ToListAsync();
        }

        public async Task<List<Amigos>> ObtenerAmigosDeUsuario(int idUsuario)
        {
            return await _context.Amigos
                .Include(a => a.Emisor)
                .Include(a => a.Receptor)
                .Where(a => (a.IdEmisor == idUsuario || a.IdReceptor == idUsuario)
                         && a.TiposEstado == TiposEstadoAmistad.Amigos)
                .ToListAsync();
        }
    }
}
