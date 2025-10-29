using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IFormModuleService
    {
        Task<IEnumerable<FormModule>> GetAllAsync();
        Task<FormModule?> GetByIdAsync(int id);
        Task<FormModule> CreateAsync(FormModule entity);
        Task<bool> UpdateAsync(FormModule entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
