using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
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
        private readonly IRepositorio<Usuario> _usuarioRepository;
        private readonly IConfiguration _config;

        public AutorizacionController(IRepositorio<Usuario> usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var usuarios = await _usuarioRepository.ObtenerTodos();
            var usuario = usuarios.FirstOrDefault(u => u.CorreoUsuario == dto.Correo && u.ContrasenaUsuario == dto.Contrasena);
            if (usuario == null)
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
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
