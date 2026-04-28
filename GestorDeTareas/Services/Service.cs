using GestorDeTareas.Interfaces;

namespace GestorDeTareas.Services
{
    public abstract class BaseService<TEntity, TDto>
    {
        protected readonly IRepositorio<TEntity> _repository;
        protected readonly Imapper<TEntity, TDto> _mapper;

        protected BaseService(IRepositorio<TEntity> repository, Imapper<TEntity, TDto> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual void Crear(TDto dto)
        {
            var idUsuarioSesion = Sesion.IdUsuarioSesionActiva;
            var entidad = _mapper.ToEntity(dto);
            _repository.Guardar(entidad);
        }


    }
}
