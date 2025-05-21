namespace BeautySalon.AuthandClient.Application.DTO;


public class RegisterUserDto
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string FullName { get; init; }
    public string Phone { get; init; }
    public string? Role { get; set; }
}
