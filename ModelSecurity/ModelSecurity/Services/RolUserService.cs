using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class RolUserService : IRolUserService
    {
        private readonly IRolUserRepository _repo;

        public RolUserService(IRolUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RolUser>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<RolUser?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<RolUser> CreateAsync(RolUser entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(RolUser entity)
        {
            var existing = await _repo.GetByIdAsync(entity.Id);
            if (existing == null) return false;

            existing.RolId = entity.RolId;
            existing.UserId = entity.UserId;
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
