using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;

namespace ModelSecurity.Repositories
{
    public class FormModuleRepository : Repository<FormModule>, IFormModuleRepository
    {
        public FormModuleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
