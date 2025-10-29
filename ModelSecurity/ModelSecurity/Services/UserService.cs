using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModelSecurity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var all = await _userRepo.GetAllAsync();
            return all;
        }

        public async Task<User?> GetByIdAsync(int id) =>
            await _userRepo.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user)
        {         
            var existing = await _userRepo.GetByEmailAsync(user.Email);
            if (existing != null) throw new InvalidOperationException("Email already registered.");

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var existing = await _userRepo.GetByIdAsync(user.Id);
            if (existing == null) return false;

            existing.Email = user.Email;
            existing.Password = user.Password;
            existing.Active = user.Active;
            existing.RegistrationDate = user.RegistrationDate;
            existing.IsDeleted = user.IsDeleted;
            existing.Person = user.Person;

            _userRepo.Update(existing);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var existing = await _userRepo.GetByIdAsync(id);
            if (existing == null) return false;
            existing.IsDeleted = true;
            _userRepo.Update(existing);
            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}
