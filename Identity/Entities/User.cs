using Handlers.Entities.Interfaces;

namespace Identity.Entities
{
    public class User: IGuidEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string? RefreshToken { get; set; }

        public bool Activated { get; set; }

        public virtual List<Role> Roles { get; set; }
        public virtual Country Country { get; set; }

    }
}
