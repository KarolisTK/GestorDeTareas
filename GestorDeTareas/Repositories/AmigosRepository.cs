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
                .Include(a => a.Usuario) 
                .Where(a => a.TiposEstado == TiposEstadoAmistad.Pendiente && a.IdUsuario2 == id)
                .Select(a => new SolicitudAmistadDto
                {
                    IdSolicitud = a.IdAmigos,
                    IdSolicitante = a.IdUsuario,
                    IdSolicitado = a.IdUsuario2,
                    NombreSolicitante = a.Usuario.NombreUsuario,
                    FechaSolicitud = a.FechaInicioAmistad,
                    Estado = a.TiposEstado
                })
                .ToListAsync();
        }

        public async Task<List<Amigos>> ObtenerAmigosDeUsuario(int idUsuario)
        {
            return await _context.Amigos
                .Include(a => a.Usuario)
                .Include(a => a.Usuario2)
                .Where(a => (a.IdUsuario == idUsuario || a.IdUsuario2 == idUsuario)
                         && a.TiposEstado == TiposEstadoAmistad.Amigos)
                .ToListAsync();
        }
    }
}
