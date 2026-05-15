using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Moq;

[TestFixture]
public class TareaUrgenteServiceTests
{
private Mock<IRepositorio<Tarea>> _repositorioBaseMock;
private Mock<IRepositorio<TareaUrgente>> _repositorioMock;
private Mock<ITareasRepository> _tareasRepositoryMock;
    private TareaUrgenteService _service;

[SetUp]
public void SetUp()
{
    _repositorioBaseMock = new Mock<IRepositorio<Tarea>>();
    _repositorioMock = new Mock<IRepositorio<TareaUrgente>>();
    _service = new TareaUrgenteService(_repositorioMock.Object, _repositorioBaseMock.Object, _tareasRepositoryMock.Object);
}

    [Test]
    public async Task PriorizarTarea_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
    _repositorioMock.Setup(r => r.ObtenerPorId(1))
        .ReturnsAsync(new TareaUrgente
        { IdTarea = 1, NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 });
    Assert.ThrowsAsync<Exception>(async () =>
        await _service.PriorizarTarea(1, null));
    }
    [Test]

    public async Task PriorizarTarea_CuandoNoEncuentraLaTarea_LanzaExcepcion()
    {
    _repositorioMock.Setup(r => r.ObtenerPorId(1))
        .ReturnsAsync(new TareaUrgente
        { IdTarea = 1, NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 });

    var dto = new CrearTareaUrgenteDTO
    {
        NombreTarea = "Tarea nueva",
        DescripcionTarea = "descripcion",
        EstadosTarea = 0,
        EstaEliminado = false,
        TiposTarea = 0,
    };

    Assert.ThrowsAsync<Exception>(async () =>
        await _service.PriorizarTarea(2, dto));
    }

    [Test]
    public async Task PriorizarTarea_EncuentraLaTareaYElDtoLlegaBien_PriorizaTarea()
    {
        _repositorioBaseMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            {
                IdTarea = 1,
                NombreTarea = "Tarea",
                DescripcionTarea = "descripcion",
                TiposTarea = TiposTarea.Simple,
                EstaEliminado = false
            });

        _repositorioBaseMock.Setup(r => r.Guardar(It.IsAny<Tarea>()))
            .Returns(Task.CompletedTask);

        _repositorioMock.Setup(r => r.Guardar(It.IsAny<TareaUrgente>()))
            .Returns(Task.CompletedTask);

        var dto = new CrearTareaUrgenteDTO { FechaLimite = DateTime.UtcNow };
        await _service.PriorizarTarea(1, dto);

        _repositorioBaseMock.Verify(r => r.Guardar(It.IsAny<Tarea>()), Times.Once);
        _repositorioMock.Verify(r => r.Guardar(It.IsAny<TareaUrgente>()), Times.Once);
    }

    [Test]
    public async Task QuitarPrioridadTarea_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new TareaUrgente
            { IdTarea = 1, NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 });
        Assert.ThrowsAsync<Exception>(async () =>
            await _service.QuitarPrioridadTarea(1, null));
    }
    [Test]

    public async Task QuitarPrioridadTarea_CuandoNoEncuentraLaTarea_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new TareaUrgente
            { IdTarea = 1, NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 });

        var dto = new CrearTareaUrgenteDTO
        {
            NombreTarea = "Tarea nueva",
            DescripcionTarea = "descripcion",
            EstadosTarea = 0,
            EstaEliminado = false,
            TiposTarea = 0,
        };

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.QuitarPrioridadTarea(2, dto));
    }

    [Test]
    public async Task QuitarPrioridadTarea_EncuentraLaTareaYElDtoLlegaBien_PriorizaTarea()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new TareaUrgente
            {
                IdTarea = 1,
                NombreTarea = "Tarea",
                DescripcionTarea = "descripcion",
                TiposTarea = TiposTarea.Urgente,
                FechaLimite = System.DateTime.UtcNow,
                EstaEliminado = false
            });

        _repositorioBaseMock.Setup(r => r.Guardar(It.IsAny<Tarea>()))
            .Returns(Task.CompletedTask);

        _repositorioMock.Setup(r => r.Guardar(It.IsAny<TareaUrgente>()))
            .Returns(Task.CompletedTask);

        var dto = new TareaDTO { };
        await _service.QuitarPrioridadTarea(1, dto);

        _repositorioBaseMock.Verify(r => r.Guardar(It.IsAny<Tarea>()), Times.Once);
        _repositorioMock.Verify(r => r.Guardar(It.IsAny<TareaUrgente>()), Times.Once);
    }

    [Test]
    public async Task CrearTareaUrgente_CuandoYaExiste_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<TareaUrgente>
            {
                new TareaUrgente { NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 }
            });

        var dto = new CrearTareaUrgenteDTO { NombreTarea = "Tarea urgente" };

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.CrearTareaUrgente(dto, 1));
    }

    [Test]
    public async Task CrearTarea_CuandoNoExiste_GuardaCorrectamente()
    {
        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<TareaUrgente>());

        _repositorioMock.Setup(r => r.Guardar(It.IsAny<TareaUrgente>()))
            .Returns(Task.CompletedTask);

        var dto = new CrearTareaUrgenteDTO
        {
            NombreTarea = "Tarea nueva",
            DescripcionTarea = "descripcion",
            EstadosTarea = 0,
            EstaEliminado = false,
            FechaCreacionTarea = DateTime.UtcNow,
            FechaLimite = DateTime.UtcNow,
            TiposTarea = 0,
            TienePrioridad = true,
            IdUsuarioDeLaTarea = 1
        };

        await _service.CrearTareaUrgente(dto, 1);

        _repositorioMock.Verify(r => r.Guardar(It.IsAny<TareaUrgente>()), Times.Once);
    }
}
