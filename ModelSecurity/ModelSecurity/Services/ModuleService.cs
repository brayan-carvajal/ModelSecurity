using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _repo;

        public ModuleService(IModuleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Module>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Module?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Module> CreateAsync(Module module)
        {
            await _repo.AddAsync(module);
            await _repo.SaveChangesAsync();
            return module;
        }

        public async Task<bool> UpdateAsync(Module module)
        {
            var existing = await _repo.GetByIdAsync(module.Id);
            if (existing == null) return false;

            existing.Name = module.Name;
            existing.Description = module.Description;
            existing.IsDeleted = module.IsDeleted;

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
