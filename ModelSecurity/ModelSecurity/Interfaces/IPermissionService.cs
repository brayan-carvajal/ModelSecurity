using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task<Permission> CreateAsync(Permission permission);
        Task<bool> UpdateAsync(Permission permission);
        Task<bool> SoftDeleteAsync(int id);
    }
}
