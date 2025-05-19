namespace BeautySalon.AuthandClient.Domain.Entity;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User() {}

    private User(string email, string passwordHash, UserRole role)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public static User Create(string email, string passwordHash, UserRole role)
    {
        return new User(email, passwordHash, role);
    }
}
