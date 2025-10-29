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
    public class RolUserController : ControllerBase
    {
        private readonly IRolUserService _rolUserService;

        public RolUserController(IRolUserService rolUserService)
        {
            _rolUserService = rolUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _rolUserService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rolUser = await _rolUserService.GetByIdAsync(id);
            return rolUser == null ? NotFound() : Ok(rolUser);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolUserDto dto)
        {
            if (dto.RolId <= 0 || dto.UserId <= 0)
                return BadRequest("Debe proporcionar RolId y UserId válidos.");

            var model = new RolUser
            {
                RolId = dto.RolId,
                UserId = dto.UserId,
                IsDeleted = dto.IsDeleted
            };

            var created = await _rolUserService.CreateAsync(model);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolUserDto dto)
        {
            var existing = await _rolUserService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.RolId = dto.RolId;
            existing.UserId = dto.UserId;
            existing.IsDeleted = dto.IsDeleted;

            await _rolUserService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _rolUserService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
