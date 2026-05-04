using GestorDeTareas;
using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Moq;

[TestFixture]
public class UsuarioServiceTests
{
    private Mock<IRepositorio<Usuario>> _repositorioMock;
    private UsuarioService _service;

    [SetUp]
    public void SetUp()
    {
        _repositorioMock = new Mock<IRepositorio<Usuario>>();
        _service = new UsuarioService(_repositorioMock.Object);
    }

    [Test]
    public async Task CrearUsuario_CuandoYaExiste_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<Usuario>
            {
                new Usuario {IdUsuario = 1 ,NombreUsuario = "nombre", CorreoUsuario = "correo@gmail.com", ContrasenaUsuario = "UsuarioContrasena21" }
            });

        var dto = new UsuarioDTO { CorreoUsuario = "correo@gmail.com" };

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.CrearUsuario(dto));
    }

    [Test]
    public async Task CrearUsuario_CuandoNoExiste_GuardaCorrectamente()
    {
        _repositorioMock.Setup(r => r.ObtenerTodos())
            .ReturnsAsync(new List<Usuario>());

        _repositorioMock.Setup(r => r.Guardar(It.IsAny<Usuario>()))
            .Returns(Task.CompletedTask);

        var dto = new UsuarioDTO
        {
            NombreUsuario = "Usuario",
            CorreoUsuario = "dfdfdfdf@gmail.com",
            ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf",
        };

        await _service.CrearUsuario(dto);

        _repositorioMock.Verify(r => r.Guardar(It.IsAny<Usuario>()), Times.Once);
    }

    [Test]
    public async Task EditaUsuario_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf" });
        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EditarUsuario(null, 1));
    }

    [Test]
    public async Task EditaUsuario_CuandoNoEncunetraUsuario_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf" });
        var dto = new EditarUsuarioDTO
        {
            NombreUsuario = "editado",
        };
        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EditarUsuario(dto, 2));
    }

    [Test]
    public async Task EditaUsuario_PuedeEditarUsuario_Edita()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario ="Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf" });
        var dto = new EditarUsuarioDTO
        {
            NombreUsuario = "editado",
        };
        await _service.EditarUsuario(dto, 1);
        var tareaEditara = await _service.ObtenerUnUsuarioPorID(1);
        Assert.That(tareaEditara.NombreUsuario, Is.EqualTo(dto.NombreUsuario));
    }

    [Test]
    public async Task EliminaUsuario_CuandoNoEncunetraUsuario_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf" ,EstaEliminado = false});

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EliminarUsuario(2));
    }

    [Test]
    public async Task EliminaUsuario_CuandoElUsuarioYaEstaEliminado_LanzaExcepcion()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf", EstaEliminado = true });

        Assert.ThrowsAsync<Exception>(async () =>
            await _service.EliminarUsuario(1));
    }

    [Test]
    public async Task EliminaUsuario_EncuentraElUsuarioYNoEstaEliminado_EliminaTarea()
    {
        _repositorioMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf", EstaEliminado = false });
        await _service.EliminarUsuario(1);
        var tareaEliminada = await _service.ObtenerUnUsuarioPorID(1);
        Assert.That(tareaEliminada.EstaEliminado, Is.EqualTo(true));
    }
}