using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Moq;

[TestFixture]
public class TareaUrgenteServiceTests
{
    private Mock<ITareasRepository> _tareasRepositoryMock;
    private Mock<IRepositorio<TareaUrgente>> _repositorioMock;
    private TareaUrgenteService _service;

    [SetUp]
    public void SetUp()
    {
        _tareasRepositoryMock = new Mock<ITareasRepository>();
        _repositorioMock = new Mock<IRepositorio<TareaUrgente>>();
        _service = new TareaUrgenteService(_repositorioMock.Object, _tareasRepositoryMock.Object);
    }

    [Test]
    public async Task PriorizarTarea_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new TareaUrgente
            { IdTarea = 1, NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 });

        Assert.That(
            async () => await _service.PriorizarTarea(1, null),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task PriorizarTarea_CuandoNoEncuentraLaTarea_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
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

        Assert.That(
            async () => await _service.PriorizarTarea(2, dto),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task PriorizarTarea_EncuentraLaTareaYElDtoLlegaBien_PriorizaTarea()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            {
                IdTarea = 1,
                NombreTarea = "Tarea",
                DescripcionTarea = "descripcion",
                TiposTarea = TiposTarea.Simple,
                EstaEliminado = false
            });

        _tareasRepositoryMock.Setup(r => r.Guardar(It.IsAny<Tarea>()))
            .Returns(Task.CompletedTask);

        _repositorioMock.Setup(r => r.Guardar(It.IsAny<TareaUrgente>()))
            .Returns(Task.CompletedTask);

        var dto = new CrearTareaUrgenteDTO { FechaLimite = DateTime.UtcNow };
        await _service.PriorizarTarea(1, dto);

        _tareasRepositoryMock.Verify(r => r.Guardar(It.IsAny<Tarea>()), Times.Once);
        _repositorioMock.Verify(r => r.Guardar(It.IsAny<TareaUrgente>()), Times.Once);
    }

    [Test]
    public async Task QuitarPrioridadTarea_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new TareaUrgente
            { IdTarea = 1, NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 });

        Assert.That(
            async () => await _service.QuitarPrioridadTarea(1, null),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task QuitarPrioridadTarea_CuandoNoEncuentraLaTarea_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new TareaUrgente
            { IdTarea = 1, NombreTarea = "Tarea urgente", IdUsuarioDeLaTarea = 1 });

        var dto = new TareaDTO
        {
            NombreTarea = "Tarea nueva",
            DescripcionTarea = "descripcion",
            EstadosTarea = 0,
            EstaEliminado = false,
            TiposTarea = 0,
        };

        Assert.That(
            async () => await _service.QuitarPrioridadTarea(2, dto),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task QuitarPrioridadTarea_EncuentraLaTareaYElDtoLlegaBien_QuitaPrioridad()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new TareaUrgente
            {
                IdTarea = 1,
                NombreTarea = "Tarea",
                DescripcionTarea = "descripcion",
                TiposTarea = TiposTarea.Urgente,
                FechaLimite = DateTime.UtcNow,
                EstaEliminado = false
            });

        _tareasRepositoryMock.Setup(r => r.Guardar(It.IsAny<Tarea>()))
            .Returns(Task.CompletedTask);

        _repositorioMock.Setup(r => r.Guardar(It.IsAny<TareaUrgente>()))
            .Returns(Task.CompletedTask);

        var dto = new TareaDTO { };
        await _service.QuitarPrioridadTarea(1, dto);

        _tareasRepositoryMock.Verify(r => r.Guardar(It.IsAny<Tarea>()), Times.Once);
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

        Assert.That(
            async () => await _service.CrearTareaUrgente(dto, 1),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task CrearTareaUrgente_CuandoNoExiste_GuardaCorrectamente()
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