namespace BeautySalon.AuthandClient.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string role);
    bool ValidateToken(string token);
}
