using Microsoft.AspNetCore.Mvc;
using ModelSecurity.Dtos;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using ModelSecurity.Services;

using Microsoft.AspNetCore.Authorization;

namespace ModelSecurity.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                Password = dto.Password,
                Active = dto.Active,
                RegistrationDate = DateTime.SpecifyKind(dto.RegistrationDate, DateTimeKind.Utc),
                IsDeleted = dto.IsDeleted,
                PersonId = dto.PersonId
            };

            var created = await _userService.CreateAsync(user);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
        {
            var existing = await _userService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Email = dto.Email;
            existing.Password = dto.Password;
            existing.Active = dto.Active;
            existing.RegistrationDate = dto.RegistrationDate;
            existing.IsDeleted = dto.IsDeleted;
            existing.PersonId = dto.PersonId;

            await _userService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _userService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
