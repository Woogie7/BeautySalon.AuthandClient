namespace BeautySalon.AuthandClient.Domain.Entity;

public class Client
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateOnly? BirthDate { get; set; }

    public User User { get; set; } = default!;
}