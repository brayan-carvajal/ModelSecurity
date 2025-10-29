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
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _formService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var form = await _formService.GetByIdAsync(id);
            return form == null ? NotFound() : Ok(form);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FormDto dto)
        {
            var form = new Form
            {
                Name = dto.Name,
                Description = dto.Description,
                IsDeleted = dto.IsDeleted
            };

            var created = await _formService.CreateAsync(form);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FormDto dto)
        {
            var existing = await _formService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.IsDeleted = dto.IsDeleted;

            await _formService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _formService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
