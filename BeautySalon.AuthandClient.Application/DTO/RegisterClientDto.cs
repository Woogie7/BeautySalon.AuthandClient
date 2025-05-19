namespace BeautySalon.AuthandClient.Application.DTO;


public class RegisterClientDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
}