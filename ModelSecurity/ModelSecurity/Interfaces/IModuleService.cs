using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllAsync();
        Task<Module?> GetByIdAsync(int id);
        Task<Module> CreateAsync(Module module);
        Task<bool> UpdateAsync(Module module);
        Task<bool> SoftDeleteAsync(int id);
    }
}
