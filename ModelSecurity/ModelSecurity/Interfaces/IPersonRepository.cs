using ModelSecurity.Models;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person?> GetByDocumentAsync(string document);
    }
}
