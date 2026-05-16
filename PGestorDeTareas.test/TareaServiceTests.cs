using GestorDeTareas;
using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using Moq;

[TestFixture]
public class TareaServiceTests
{
    private Mock<ITareasRepository> _tareasRepositoryMock;
    private TareaService _service;

    [SetUp]
    public void SetUp()
    {
        _tareasRepositoryMock = new Mock<ITareasRepository>();
        _service = new TareaService(_tareasRepositoryMock.Object);
    }

    [Test]
    public async Task MostrarTarea_CuandoNoEncunetraTarea_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea no encontrada", IdUsuarioDeLaTarea = 1 });

        Assert.That(
            async () => await _service.ObtenerUnaTareaPorID(2),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task MostrarTarea_EncuentraLaTarea_MuestraLaTarea()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1 });

        var resultado = await _service.ObtenerUnaTareaPorID(1);
        Assert.That(resultado is not null);
        Assert.That(resultado.IdTarea, Is.EqualTo(1));
    }

    [Test]
    public async Task CrearTarea_CuandoYaExiste_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<Tarea>
            {
                new Tarea { NombreTarea = "Tarea repetida", IdUsuarioDeLaTarea = 1 }
            });

        var dto = new TareaDTO { NombreTarea = "Tarea repetida" };

        Assert.That(
            async () => await _service.CrearTarea(dto, 1),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task CrearTarea_CuandoNoExiste_GuardaCorrectamente()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<Tarea>());

        _tareasRepositoryMock.Setup(r => r.Guardar(It.IsAny<Tarea>()))
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

        _tareasRepositoryMock.Verify(r => r.Guardar(It.IsAny<Tarea>()), Times.Once);
    }

    [Test]
    public async Task EditaTarea_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1 });

        Assert.That(
            async () => await _service.EditarTarea(1, null, 1),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EditaTarea_CuandoNoEncunetraTarea_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
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

        Assert.That(
            async () => await _service.EditarTarea(2, dto, 1),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EditaTarea_PuedeEditarTarea_Edita()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
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

        var tareaEditada = await _service.ObtenerUnaTareaPorID(1);
        Assert.That(tareaEditada.NombreTarea, Is.EqualTo(dto.NombreTarea));
    }

    [Test]
    public async Task EliminaTarea_CuandoNoEncunetraTarea_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea no encontrada", IdUsuarioDeLaTarea = 1, EstaEliminado = false });

        Assert.That(
            async () => await _service.EliminarTarea(2),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EliminaTarea_CuandoLaTareaYaEstaEliminada_LanzaExcepcion()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1, EstaEliminado = true });

        Assert.That(
            async () => await _service.EliminarTarea(1),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EliminaTarea_EncuentraLaTareaYNoEstaEliminada_EliminaTarea()
    {
        _tareasRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Tarea
            { IdTarea = 1, NombreTarea = "Tarea encontrada", IdUsuarioDeLaTarea = 1, EstaEliminado = false });

        await _service.EliminarTarea(1);

        var tareaEliminada = await _service.ObtenerUnaTareaPorID(1);
        Assert.That(tareaEliminada.EstaEliminado, Is.EqualTo(true));
    }
}