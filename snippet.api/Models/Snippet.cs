using System.ComponentModel.DataAnnotations;

namespace snippet.api.Models;

public class Snippet
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Language { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}