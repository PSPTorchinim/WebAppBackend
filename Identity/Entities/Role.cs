using Handlers.Entities.Interfaces;

namespace Identity.Entities
{
    public class Role: IGuidEntity, INamedEntity, IDescriptionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Role> Extends { get; set;}
        public List<Permission> Permissions { get; set;}
    }
}
