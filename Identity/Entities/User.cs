using Handlers.Entities.Interfaces;

namespace Identity.Entities
{
    public class User: IGuidEntity, INamedEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string RefreshToken { get; set; }

        public virtual List<Role> Roles { get; set; }
        public Country Country { get; set; }

    }
}
