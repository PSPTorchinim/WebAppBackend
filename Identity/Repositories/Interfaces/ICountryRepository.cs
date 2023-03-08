using Handlers.Repositories;
using Identity.Entities;

namespace Identity.Repositories.Interfaces
{
    public interface ICountryRepository: IRepository<Country>
    {
        Task<Country> GetByShortcout(string shortcut);
    }
}
