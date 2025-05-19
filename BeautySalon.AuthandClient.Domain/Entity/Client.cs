namespace BeautySalon.AuthandClient.Domain.Entity;

public class Client
{
    public Guid Id { get; set; }
    public Guid UserId { get; private set; } // FK → User
    public string FullName { get; private set; }
    public string Phone { get; private set; }

    private Client() { }

    private Client(Guid userId, string fullName, string phone)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FullName = fullName;
        Phone = phone;
    }

    public static Client Create(Guid userId, string fullName, string phone)
    {
        return new Client(userId, fullName, phone);
    }
}