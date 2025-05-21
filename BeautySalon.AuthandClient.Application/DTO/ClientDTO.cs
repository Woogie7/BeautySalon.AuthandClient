namespace BeautySalon.AuthandClient.Application.DTO;

public class ClientDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; } = default!;
    public string Phone { get; set; } = default!;
}
