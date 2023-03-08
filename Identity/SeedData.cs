using Handlers.Helpers;
using Identity.Entities;
using Identity.Repositories.Interfaces;

namespace Identity
{
    public class SeedData
    {

        private readonly IUsersRepository usersRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IRolesRepository rolesRepository;
        private readonly IPermissionsRepository permissionsRepository;


        public SeedData(IUsersRepository usersRepository, ICountryRepository countryRepository, IRolesRepository rolesRepository, IPermissionsRepository permissionsRepository)
        {
            this.usersRepository = usersRepository;
            this.countryRepository = countryRepository;
            this.rolesRepository = rolesRepository;
            this.permissionsRepository = permissionsRepository;
        }

        public async Task InitializeAsync()
        {
            if (await countryRepository.Empty()) await SeedCountries();
            if (await permissionsRepository.Empty()) await SeedPermissions();
            if (await rolesRepository.Empty()) await SeedRoles();
            if (await usersRepository.Empty()) await SeedUsers();
        }

        private async Task SeedRoles()
        {
            await rolesRepository.Add(new Role() { 
                Name = "SuperOwner", 
                Description = "Full access to all functions", 
                Permissions = await permissionsRepository.GetAsync() 
            });
        }

        private async Task SeedPermissions()
        {
            await permissionsRepository.Add(new Permission() { 
                Name = "Full", 
                Description = "Full access to all functions" 
            });
        }

        private async Task SeedCountries()
        {
            await countryRepository.Add(new Country() {
                Name = "Poland", 
                Shortcut = "pl-PL" 
            });
            await countryRepository.Add(new Country() { 
                Name = "United States", 
                Shortcut = "en-US" 
            });
        }

        private async Task SeedUsers()
        {
            await usersRepository.Add(new User()
            {
                Username = "PSPTorchinim",
                Password = Converter.computeHash("1111PSPTorchinim448"),
                Email = "huberttroc@gmail.com",
                Gender = Gender.Male,
                Country = await countryRepository.GetByShortcut("pl-PL"),
                Roles = await rolesRepository.GetAsync()
            });
        }
    }
}
