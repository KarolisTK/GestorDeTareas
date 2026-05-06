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

        public virtual async Task Crear(TDto dto, int IdUsuario)
        {
            var idUsuarioSesion = IdUsuario ;
            var entidad = _mapper.ToEntity(dto);
            await _repository.Guardar(entidad);
        }


    }
}
