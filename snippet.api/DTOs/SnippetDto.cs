using System.ComponentModel.DataAnnotations;

namespace snippet.api.DTOs;

// For creating new snippets
public class CreateSnippetDto
{
    [Required]
    [MaxLength(50)]
    public string Language { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }
}

// For updating snippets
public class UpdateSnippetDto
{
    [MaxLength(50)]
    public string? Language { get; set; }

    public string? Code { get; set; }

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }
}

// For returning snippets
public class SnippetDto
{
    public Guid Id { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}