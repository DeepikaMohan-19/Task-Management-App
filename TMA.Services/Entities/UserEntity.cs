namespace TMA.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
