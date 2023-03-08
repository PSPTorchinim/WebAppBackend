using Handlers.Repositories;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementations
{
    public class PermissionsRepository : Repository<Permission>, IPermissionsRepository
    {
        public PermissionsRepository(DbContext context) : base(context)
        {
        }
    }
}
