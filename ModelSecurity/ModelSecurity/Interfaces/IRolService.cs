using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetAllAsync();
        Task<Rol?> GetByIdAsync(int id);
        Task<Rol> CreateAsync(Rol rol);
        Task<bool> UpdateAsync(Rol rol);
        Task<bool> SoftDeleteAsync(int id);
    }
}
