using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestorDeTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorizacionController : Controller
    {
        private readonly UsuarioService _usuarioservice;
        private readonly IConfiguration _config;

        public AutorizacionController(UsuarioService usuarioService, IConfiguration config)
        {
            _usuarioservice = usuarioService;
            _config = config;
        }

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var usuario = await _usuarioservice.ObtenerUsuarioPorCorreo(dto.Correo);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.ContrasenaUsuario))
                return Unauthorized("Credenciales incorrectas");

            var token = GenerarToken(usuario);
            return Ok(new { token });
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.CorreoUsuario)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
