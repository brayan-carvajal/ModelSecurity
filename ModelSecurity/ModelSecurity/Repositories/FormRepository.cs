using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Models;

namespace ModelSecurity.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        public FormRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
