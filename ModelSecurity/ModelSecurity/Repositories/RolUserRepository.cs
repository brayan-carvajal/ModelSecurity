using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;

namespace ModelSecurity.Repositories
{
    public class RolUserRepository : Repository<RolUser>, IRolUserRepository
    {
        public RolUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
