using Identity.DTO.Permission;

namespace Identity.DTO.Role
{
    public class GetRolesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetPermissionsDTO> Permissions { get; set; }
    }
}
