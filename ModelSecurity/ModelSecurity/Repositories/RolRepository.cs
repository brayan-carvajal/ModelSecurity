using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;

namespace ModelSecurity.Repositories
{
    public class RolRepository : Repository<Rol>, IRolRepository
    {
        public RolRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
