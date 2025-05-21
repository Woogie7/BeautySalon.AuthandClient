namespace BeautySalon.AuthandClient.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string role, string email);
    bool ValidateToken(string token);
}
