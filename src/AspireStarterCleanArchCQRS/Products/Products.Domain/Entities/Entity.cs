namespace Products.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }

    public string CreatedBy { get; set; } = "Admin";

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; } = "Admin";

    public DateTime? ModifiedDate { get; set; }
}
