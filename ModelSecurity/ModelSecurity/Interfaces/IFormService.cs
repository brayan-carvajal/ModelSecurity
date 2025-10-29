using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IFormService
    {
        Task<IEnumerable<Form>> GetAllAsync();
        Task<Form?> GetByIdAsync(int id);
        Task<Form> CreateAsync(Form form);
        Task<bool> UpdateAsync(Form form);
        Task<bool> SoftDeleteAsync(int id);
    }
}
