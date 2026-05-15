using GestorDeTareas;
using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using Moq;

[TestFixture]
public class TareaServiceTests
{
    private Mock<IRepositorio<Tarea>> _repositorioMock;
    private TareaService _service;
    private Mock<ITareasRepository> _tareasRepositoryMock;
    [SetUp]
    public void SetUp()
    {
        _repositorioMock = new Mock<IRepositorio<Tarea>>();
        _service = new TareaService(_repositorioMock.Object, _tareasRepositoryMock.Object);
    }

    [Test]
    public async Task MostrarTarea_CuandoNoEncunetraTarea_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
              { IdTarea = 1 ,NombreTarea = "Tarea no encontrada", IdUsuarioDeLaTarea = 1 });

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.ObtenerUnaTareaPorID(2));
    }

    [Test]
    public async Task MostrarTarea_EncuentraLaTarea_MuestraLaTarea()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1 });

        var resultado = await _service.ObtenerUnaTareaPorID(1);
        Assert.That(resultado is not null);
        Assert.That(resultado.IdTarea, Is.EqualTo(1));
    }

    [Test]
    public async Task CrearTarea_CuandoYaExiste_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<Tarea>
            {
                new Tarea { NombreTarea = "Tarea repetida", IdUsuarioDeLaTarea = 1 }
            });

        var dto = new TareaDTO { NombreTarea = "Tarea repetida" };

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.CrearTarea(dto, 1));
    }

    [Test]
    public async Task CrearTarea_CuandoNoExiste_GuardaCorrectamente()
    {
        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<Tarea>());

        _repositorioMock.Setup(r => r.Guardar(It.IsAny<Tarea>()))
            .Returns(Task.CompletedTask);

        var dto = new TareaDTO
        {
            NombreTarea = "Tarea nueva",
            DescripcionTarea = "descripcion",
            EstadosTarea = 0,
            EstaEliminado = false,
            FechaCreacionTarea = DateTime.UtcNow,
            TiposTarea = 0,
            IdUsuarioDeLaTarea = 1
        };

        await _service.CrearTarea(dto, 1);

        _repositorioMock.Verify(r => r.Guardar(It.IsAny<Tarea>()), Times.Once);
    }

    [Test]
    public async Task EditaTarea_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1 });
        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EditarTarea(1, null, 1));
    }

    [Test]
    public async Task EditaTarea_CuandoNoEncunetraTarea_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea no encontrada", IdUsuarioDeLaTarea = 1 });
        var dto = new EditarTareaDTO
        {
            NombreTarea = "Tarea nueva",
            DescripcionTarea = "descripcion",
            EstadosTarea = 0,
            EstaEliminado = false,
            TiposTarea = 0,
        };
        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EditarTarea(2, dto, 1));
    }

    [Test]
    public async Task EditaTarea_PuedeEditarTarea_Edita()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1 });
        var dto = new EditarTareaDTO
        {
            NombreTarea = "Tarea nueva",
            DescripcionTarea = "descripcion",
            EstadosTarea = 0,
            EstaEliminado = false,
            TiposTarea = 0,
        };
        await _service.EditarTarea(1, dto, 1);
        var tareaEditara = await _service.ObtenerUnaTareaPorID(1);
        Assert.That(tareaEditara.NombreTarea, Is.EqualTo(dto.NombreTarea));
    }

    [Test]
    public async Task EliminaTarea_CuandoNoEncunetraTarea_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea no encontrada", IdUsuarioDeLaTarea = 1, EstaEliminado = false });

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EliminarTarea(2));
    }

    [Test]
    public async Task EliminaTarea_CuandoLaTareaYaEstaEliminada_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1, EstaEliminado = true });

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EliminarTarea(1));
    }

    [Test]
    public async Task EliminaTarea_EncuentraLaTareaYNoEstaEliminada_EliminaTarea()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1, EstaEliminado = false });
        await _service.EliminarTarea(1);
        var tareaEliminada = await _service.ObtenerUnaTareaPorID(1);
        Assert.That(tareaEliminada.EstaEliminado, Is.EqualTo(true));
    }
}