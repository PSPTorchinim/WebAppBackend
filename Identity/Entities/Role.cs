using Handlers.Entities.Interfaces;

namespace Identity.Entities
{
    public class Role: IGuidEntity, INamedEntity, IDescriptionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Permission> Permissions { get; set;}
        public virtual List<User> Users { get; set; }
    }
}
