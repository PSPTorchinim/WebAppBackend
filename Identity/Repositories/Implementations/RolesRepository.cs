using Handlers.Repositories;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementations
{
    public class RolesRepository : Repository<Role, IdentityContext>, IRolesRepository
    {
        public RolesRepository(IdentityContext context) : base(context)
        {
        }
    }
}
