using Identity.Entities;

namespace Identity.DTO.User
{
    public class RegisterUserRequestDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string CountryShortcut { get; set; }
    }
}
