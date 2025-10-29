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
    public class FormModuleController : ControllerBase
    {
        private readonly IFormModuleService _formModuleService;

        public FormModuleController(IFormModuleService formModuleService)
        {
            _formModuleService = formModuleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _formModuleService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var formModule = await _formModuleService.GetByIdAsync(id);
            return formModule == null ? NotFound() : Ok(formModule);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FormModuleDto dto)
        {
            if (dto.FormId <= 0 || dto.ModuleId <= 0)
                return BadRequest("Debe proporcionar FormId y ModuleId válidos.");

            var model = new FormModule
            {
                FormId = dto.FormId,
                ModuleId = dto.ModuleId,
                IsDeleted = dto.IsDeleted
            };

            var created = await _formModuleService.CreateAsync(model);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FormModuleDto dto)
        {
            var existing = await _formModuleService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.FormId = dto.FormId;
            existing.ModuleId = dto.ModuleId;
            existing.IsDeleted = dto.IsDeleted;

            await _formModuleService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _formModuleService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
