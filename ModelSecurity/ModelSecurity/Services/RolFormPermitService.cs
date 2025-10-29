using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class RolFormPermitService : IRolFormPermitService
    {
        private readonly IRolFormPermitRepository _repo;

        public RolFormPermitService(IRolFormPermitRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RolFormPermit>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<RolFormPermit?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<RolFormPermit> CreateAsync(RolFormPermit entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(RolFormPermit entity)
        {
            var existing = await _repo.GetByIdAsync(entity.Id);
            if (existing == null) return false;

            existing.RolId = entity.RolId;
            existing.PermissionId = entity.PermissionId;
            existing.FormId = entity.FormId;
            existing.IsDeleted = entity.IsDeleted;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsDeleted = true;
            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
