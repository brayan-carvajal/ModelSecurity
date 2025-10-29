using Microsoft.EntityFrameworkCore;
using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Threading.Tasks;

namespace ModelSecurity.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }  
    }
}
