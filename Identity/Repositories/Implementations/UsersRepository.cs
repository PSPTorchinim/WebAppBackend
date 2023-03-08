using Handlers.Repositories;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementations
{
    public class UsersRepository : Repository<User, IdentityContext>, IUsersRepository
    {
        public UsersRepository(IdentityContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAsync()
        {
            return context.Users
                .Include(u => u.Roles).ThenInclude(r => r.Permissions)
                .Include(u => u.Country)
                .ToList();
        }
    }
}
