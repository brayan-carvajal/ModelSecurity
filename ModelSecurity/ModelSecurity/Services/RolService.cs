using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repo;

        public RolService(IRolRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Rol> CreateAsync(Rol rol)
        {
            await _repo.AddAsync(rol);
            await _repo.SaveChangesAsync();
            return rol;
        }

        public async Task<bool> UpdateAsync(Rol rol)
        {
            var existing = await _repo.GetByIdAsync(rol.Id);
            if (existing == null) return false;

            existing.Name = rol.Name;
            existing.Description = rol.Description;
            existing.UserId = rol.UserId;
            existing.IsDeleted = rol.IsDeleted;

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
