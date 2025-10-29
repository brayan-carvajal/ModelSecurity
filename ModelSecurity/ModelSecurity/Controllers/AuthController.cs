using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelSecurity.Data;
using ModelSecurity.Dtos;
using ModelSecurity.Services;

using Microsoft.AspNetCore.Authorization;

namespace ModelSecurity.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;

        public AuthController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Debe enviar el email y la contraseña.");

            var normalizedEmail = dto.Email.Trim().ToLower();

            var user = await _context.User
                .Include(u => u.RolUser)
                .ThenInclude(ru => ru.Rol)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail && u.Password == dto.Password);

            if (user == null)
                return Unauthorized("Credenciales inválidas.");

            var roles = user.RolUser.Select(r => r.Rol.Name).ToList();

            var token = _authService.GenerateJwtToken(user, roles);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Roles = roles
            });
        }

    }
}
