using BeautySalon.AuthandClient.Application.DTO;

namespace BeautySalon.AuthandClient.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterClientAsync(RegisterUserDto dto);
    Task<AuthResponseDto> LoginAsync(string email, string password);
    Task<bool> ValidateTokenAsync(string token);
}

