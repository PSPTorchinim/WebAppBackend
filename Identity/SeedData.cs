using Handlers.Helpers;
using Identity.Entities;
using Identity.Repositories.Interfaces;

namespace Identity
{
    public class SeedData
    {

        private readonly IUsersRepository usersRepository;
        private readonly ICountryRepository countryRepository;

        public SeedData(IUsersRepository usersRepository, ICountryRepository countryRepository)
        {
            this.usersRepository = usersRepository;
            this.countryRepository = countryRepository;
        }

        public async Task InitializeAsync(IServiceProvider services)
        {
            if (await countryRepository.Empty()) await SeedCountries();
            if (await usersRepository.Empty()) await SeedUsers();
        }

        private async Task SeedCountries()
        {
            await countryRepository.Add(new Country() { Name = "Poland", Shortcut = "pl-PL" });
            await countryRepository.Add(new Country() { Name = "United States", Shortcut = "en-US" });
        }

        private async Task SeedUsers()
        {
            await usersRepository.Add(new User()
            {
                Username = "PSPTorchinim",
                Password = Converter.computeHash("1111PSPTorchinim448"),
                Email = "psptorchinim@gmail.com",
                Gender = Gender.Male,
                Country = await countryRepository.GetByShortcout("pl-PL"),
                Roles = new List<Role>() {
                    
                }
            });
        }
    }
}
