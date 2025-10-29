using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepo;

        public PersonService(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        public async Task<IEnumerable<Person>> GetAllAsync() =>
            await _personRepo.GetAllAsync();

        public async Task<Person?> GetByIdAsync(int id) =>
            await _personRepo.GetByIdAsync(id);

        public async Task<Person> CreateAsync(Person person)
        {
            await _personRepo.AddAsync(person);
            await _personRepo.SaveChangesAsync();
            return person;
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            var existing = await _personRepo.GetByIdAsync(person.Id);
            if (existing == null) return false;
           
            existing.FirstName = person.FirstName;
            existing.LastName = person.LastName;
            existing.Document_type = person.Document_type;
            existing.Document = person.Document;
            existing.DateBorn = person.DateBorn;
            existing.PhoneNumber = person.PhoneNumber;
            existing.Gender = person.Gender;
            existing.PersonExter = person.PersonExter;
            existing.EpsId = person.EpsId;
            existing.SecondLastName = person.SecondLastName;
            existing.MiddleName = person.MiddleName;
            existing.Active = person.Active;
            existing.CityId = person.CityId;

            _personRepo.Update(existing);
            await _personRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var existing = await _personRepo.GetByIdAsync(id);
            if (existing == null) return false;
            existing.Active = false; 
            _personRepo.Update(existing);
            await _personRepo.SaveChangesAsync();
            return true;
        }
    }
}
