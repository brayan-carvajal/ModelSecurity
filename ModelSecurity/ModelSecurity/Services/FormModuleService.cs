using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class FormModuleService : IFormModuleService
    {
        private readonly IFormModuleRepository _repo;

        public FormModuleService(IFormModuleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FormModule>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<FormModule?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<FormModule> CreateAsync(FormModule entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(FormModule entity)
        {
            var existing = await _repo.GetByIdAsync(entity.Id);
            if (existing == null) return false;

            existing.FormId = entity.FormId;
            existing.ModuleId = entity.ModuleId;
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
