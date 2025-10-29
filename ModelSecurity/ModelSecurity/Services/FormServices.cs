using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _repo;

        public FormService(IFormRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Form>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Form?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Form> CreateAsync(Form form)
        {
            await _repo.AddAsync(form);
            await _repo.SaveChangesAsync();
            return form;
        }

        public async Task<bool> UpdateAsync(Form form)
        {
            var existing = await _repo.GetByIdAsync(form.Id);
            if (existing == null) return false;

            existing.Name = form.Name;
            existing.Description = form.Description;
            existing.IsDeleted = form.IsDeleted;

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
