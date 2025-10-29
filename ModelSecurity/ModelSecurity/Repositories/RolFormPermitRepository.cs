using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;

namespace ModelSecurity.Repositories
{
    public class RolFormPermitRepository : Repository<RolFormPermit>, IRolFormPermitRepository
    {
        public RolFormPermitRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
