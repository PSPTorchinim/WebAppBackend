using Handlers.Repositories;
using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<List<User>> GetAsync();
    }
}
