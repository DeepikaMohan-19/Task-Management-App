namespace TMA.Application.DTOs.Authentication
{
    public class UserDto : BaseDto
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
