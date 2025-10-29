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
    public class RolFormPermitController : ControllerBase
    {
        private readonly IRolFormPermitService _rolFormPermitService;

        public RolFormPermitController(IRolFormPermitService rolFormPermitService)
        {
            _rolFormPermitService = rolFormPermitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _rolFormPermitService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _rolFormPermitService.GetByIdAsync(id);
            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolFormPermitDto dto)
        {
            if (dto.RolId <= 0 || dto.FormId <= 0 || dto.PermissionId <= 0)
                return BadRequest("Debe proporcionar RolId, FormId y PermissionId válidos.");

            var model = new RolFormPermit
            {
                RolId = dto.RolId,
                FormId = dto.FormId,
                PermissionId = dto.PermissionId,
                IsDeleted = dto.IsDeleted
            };

            var created = await _rolFormPermitService.CreateAsync(model);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolFormPermitDto dto)
        {
            var existing = await _rolFormPermitService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.RolId = dto.RolId;
            existing.FormId = dto.FormId;
            existing.PermissionId = dto.PermissionId;
            existing.IsDeleted = dto.IsDeleted;

            await _rolFormPermitService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _rolFormPermitService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
