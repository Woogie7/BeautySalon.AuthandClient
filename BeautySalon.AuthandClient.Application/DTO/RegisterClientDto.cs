using System.ComponentModel.DataAnnotations;

namespace BeautySalon.AuthandClient.Application.DTO;


public class RegisterUserDto
{
    [Required, EmailAddress]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }

    [Required, MinLength(2)]
    public string FirstName { get; init; }

    [Required, MinLength(2)]
    public string LastName { get; init; }

    [Required]
    public string Phone { get; init; }

    public string? Role { get; set; }
}

