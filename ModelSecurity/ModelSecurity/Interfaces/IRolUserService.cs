using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IRolUserService
    {
        Task<IEnumerable<RolUser>> GetAllAsync();
        Task<RolUser?> GetByIdAsync(int id);
        Task<RolUser> CreateAsync(RolUser entity);
        Task<bool> UpdateAsync(RolUser entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
