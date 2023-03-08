using Handlers.Repositories;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories.Implementations
{
    public class CountryRepository : Repository<Country, IdentityContext>, ICountryRepository
    {
        public CountryRepository(IdentityContext context) : base(context)
        {
        }

        public async Task<Country> GetByShortcut(string shortcut)
        {
            var x = await GetAsync(c => c.Shortcut.Equals(shortcut));
            return x.FirstOrDefault();
        }
    }
}
