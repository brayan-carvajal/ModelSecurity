using ModelSecurity.Models;
using System.Threading.Tasks;

namespace ModelSecurity.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
