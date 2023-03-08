using Handlers.Entities.Interfaces;

namespace Identity.Entities
{
    public class Permission : IGuidEntity, INamedEntity, IDescriptionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}
