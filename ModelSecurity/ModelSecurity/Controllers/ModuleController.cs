using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelSecurity.Dtos;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using ModelSecurity.Services;

using Microsoft.AspNetCore.Authorization;

namespace ModelSecurity.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _moduleService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var module = await _moduleService.GetByIdAsync(id);
            return module == null ? NotFound() : Ok(module);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ModuleDto dto)
        {
            var module = new Module
            {
                Name = dto.Name,
                Description = dto.Description,
                IsDeleted = dto.IsDeleted
            };

            var created = await _moduleService.CreateAsync(module);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModuleDto dto)
        {
            var existing = await _moduleService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.IsDeleted = dto.IsDeleted;

            await _moduleService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _moduleService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
