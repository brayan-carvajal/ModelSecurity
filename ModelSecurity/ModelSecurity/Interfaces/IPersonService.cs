using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> SoftDeleteAsync(int id);
    }
}
