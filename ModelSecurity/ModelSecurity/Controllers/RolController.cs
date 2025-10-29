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
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _rolService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rol = await _rolService.GetByIdAsync(id);
            return rol == null ? NotFound() : Ok(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolDto dto)
        {
            var rol = new Rol
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = dto.UserId,
                IsDeleted = dto.IsDeleted
            };

            var created = await _rolService.CreateAsync(rol);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolDto dto)
        {
            var existing = await _rolService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.UserId = dto.UserId;
            existing.IsDeleted = dto.IsDeleted;

            await _rolService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _rolService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
