using Handlers.Repositories;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementations
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(DbContext context) : base(context)
        {
        }
    }
}
