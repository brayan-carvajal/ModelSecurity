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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _permissionService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            return permission == null ? NotFound() : Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermissionDto dto)
        {
            var permission = new Permission
            {
                Name = dto.Name,
                Description = dto.Description,
                IsDeleted = dto.IsDeleted
            };

            var created = await _permissionService.CreateAsync(permission);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionDto dto)
        {
            var existing = await _permissionService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.IsDeleted = dto.IsDeleted;

            await _permissionService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _permissionService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
