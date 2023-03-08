using Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class IdentityContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Country> Countries { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
        {
            Database.Migrate();
        }
    }
}
