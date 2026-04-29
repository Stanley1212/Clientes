namespace OrionTek.API.Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}