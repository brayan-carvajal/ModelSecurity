using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repo;

        public PermissionService(IPermissionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Permission> CreateAsync(Permission permission)
        {
            await _repo.AddAsync(permission);
            await _repo.SaveChangesAsync();
            return permission;
        }

        public async Task<bool> UpdateAsync(Permission permission)
        {
            var existing = await _repo.GetByIdAsync(permission.Id);
            if (existing == null) return false;

            existing.Name = permission.Name;
            existing.Description = permission.Description;
            existing.IsDeleted = permission.IsDeleted;

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
