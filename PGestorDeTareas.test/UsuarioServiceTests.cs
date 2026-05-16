using GestorDeTareas;
using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Moq;

[TestFixture]
public class UsuarioServiceTests
{
    private Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private UsuarioService _service;

    [SetUp]
    public void SetUp()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _service = new UsuarioService(_usuarioRepositoryMock.Object);
    }

    [Test]
    public async Task CrearUsuario_CuandoYaExiste_LanzaExcepcion()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorCorreo("correo@gmail.com"))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "nombre", CorreoUsuario = "correo@gmail.com", ContrasenaUsuario = "UsuarioContrasena21" });

        var dto = new UsuarioDTO { CorreoUsuario = "correo@gmail.com" };

        Assert.That(
            async () => await _service.CrearUsuario(dto),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task CrearUsuario_CuandoNoExiste_GuardaCorrectamente()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorCorreo("dfdfdfdf@gmail.com"))
            .ReturnsAsync((Usuario)null);

        _usuarioRepositoryMock.Setup(r => r.ObtenerPorFriendTag(It.IsAny<string>()))
            .ReturnsAsync((Usuario)null);

        _usuarioRepositoryMock.Setup(r => r.Guardar(It.IsAny<Usuario>()))
            .Returns(Task.CompletedTask);

        var dto = new UsuarioDTO
        {
            NombreUsuario = "Usuario",
            CorreoUsuario = "dfdfdfdf@gmail.com",
            ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf",
        };

        await _service.CrearUsuario(dto);

        _usuarioRepositoryMock.Verify(r => r.Guardar(It.IsAny<Usuario>()), Times.Once);
    }

    [Test]
    public async Task EditaUsuario_CuandoElDTOLlegaNulo_LanzaExcepcion()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf" });

        Assert.That(
            async () => await _service.EditarUsuario(null, 1),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EditaUsuario_CuandoNoEncunetraUsuario_LanzaExcepcion()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf" });

        var dto = new EditarUsuarioDTO { NombreUsuario = "editado" };

        Assert.That(
            async () => await _service.EditarUsuario(dto, 2),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EditaUsuario_PuedeEditarUsuario_Edita()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf" });

        var dto = new EditarUsuarioDTO { NombreUsuario = "editado" };

        await _service.EditarUsuario(dto, 1);

        var usuarioEditado = await _service.ObtenerUnUsuarioPorID(1);
        Assert.That(usuarioEditado.NombreUsuario, Is.EqualTo(dto.NombreUsuario));
    }

    [Test]
    public async Task EliminaUsuario_CuandoNoEncunetraUsuario_LanzaExcepcion()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf", EstaEliminado = false });

        Assert.That(
            async () => await _service.EliminarUsuario(2),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EliminaUsuario_CuandoElUsuarioYaEstaEliminado_LanzaExcepcion()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf", EstaEliminado = true });

        Assert.That(
            async () => await _service.EliminarUsuario(1),
            Throws.InstanceOf<Exception>());
    }

    [Test]
    public async Task EliminaUsuario_EncuentraElUsuarioYNoEstaEliminado_EliminaUsuario()
    {
        _usuarioRepositoryMock.Setup(r => r.ObtenerPorId(1))
            .ReturnsAsync(new Usuario
            { IdUsuario = 1, NombreUsuario = "Usuario", CorreoUsuario = "dfdfdfdf@gmail.com", ContrasenaUsuario = "dfdfdfdfdfdfdfdfdfdf", EstaEliminado = false });

        await _service.EliminarUsuario(1);

        var usuarioEliminado = await _service.ObtenerUnUsuarioPorID(1);
        Assert.That(usuarioEliminado.EstaEliminado, Is.EqualTo(true));
    }
}