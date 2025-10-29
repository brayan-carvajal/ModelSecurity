using Microsoft.EntityFrameworkCore;
using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;
using System.Threading.Tasks;

namespace ModelSecurity.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<Person?> GetByDocumentAsync(string document)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Document == document);
        }
    }
}
