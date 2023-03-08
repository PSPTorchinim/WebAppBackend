using Handlers.Entities.Interfaces;

namespace Identity.Entities
{
    public class Country: IGuidEntity, INamedEntity
    {
        public Guid Id { get; set; }
        public string Shortcut { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
