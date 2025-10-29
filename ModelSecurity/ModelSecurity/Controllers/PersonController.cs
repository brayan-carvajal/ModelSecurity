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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _personService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            return person == null ? NotFound() : Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonDto dto)
        {
            var person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Document_type = dto.Document_type,
                Document = dto.Document,
                DateBorn = DateTime.SpecifyKind(dto.DateBorn, DateTimeKind.Utc),
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                PersonExter = dto.PersonExter,
                EpsId = dto.EpsId,
                SecondLastName = dto.SecondLastName,
                MiddleName = dto.MiddleName,
                Active = dto.Active,
                CityId = dto.CityId
            };

            var created = await _personService.CreateAsync(person);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonDto dto)
        {
            var existing = await _personService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Document_type = dto.Document_type;
            existing.Document = dto.Document;
            existing.DateBorn = dto.DateBorn;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.Gender = dto.Gender;
            existing.PersonExter = dto.PersonExter;
            existing.EpsId = dto.EpsId;
            existing.SecondLastName = dto.SecondLastName;
            existing.MiddleName = dto.MiddleName;
            existing.Active = dto.Active;
            existing.CityId = dto.CityId;

            await _personService.UpdateAsync(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var deleted = await _personService.SoftDeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
