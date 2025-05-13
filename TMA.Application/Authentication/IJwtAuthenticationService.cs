using TMA.Application.DTOs.Authentication;

namespace TMA.Application.Authentication
{
    public interface IJwtAuthenticationService
    {
        string GenerateJwtToken(UserDto user);
    }
}
