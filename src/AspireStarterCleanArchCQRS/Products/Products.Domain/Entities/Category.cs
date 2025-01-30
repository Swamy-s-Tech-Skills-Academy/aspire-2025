using System.ComponentModel.DataAnnotations;

namespace Products.Domain.Entities;

public class Category : Entity
{
    [MaxLength(255)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
}
