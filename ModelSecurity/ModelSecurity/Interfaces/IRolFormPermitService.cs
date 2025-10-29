using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IRolFormPermitService
    {
        Task<IEnumerable<RolFormPermit>> GetAllAsync();
        Task<RolFormPermit?> GetByIdAsync(int id);
        Task<RolFormPermit> CreateAsync(RolFormPermit entity);
        Task<bool> UpdateAsync(RolFormPermit entity);
        Task<bool> SoftDeleteAsync(int id);
    }
}
