using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;

namespace ModelSecurity.Repositories
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(ApplicationDbContext ctx) : base(ctx)
        {
        }
    }
}
