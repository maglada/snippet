using Microsoft.EntityFrameworkCore;
using snippet.api.Data;
using snippet.api.DTOs;
using snippet.api.Models;

namespace snippet.api.Services;

public class SnippetService : ISnippetService
{
    private readonly SnippetContext _context;

    public SnippetService(SnippetContext context)
    {
        _context = context;
    }

    public async Task<SnippetDto> CreateSnippetAsync(CreateSnippetDto createDto)
    {
        var snippet = new Snippet
        {
            Id = Guid.NewGuid(),
            Language = createDto.Language,
            Code = createDto.Code,
            Title = createDto.Title,
            Description = createDto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Snippets.Add(snippet);
        await _context.SaveChangesAsync();

        return MapToDto(snippet);
    }

    public async Task<IEnumerable<SnippetDto>> GetSnippetsAsync(string? language = null)
    {
        var query = _context.Snippets.AsQueryable();

        if (!string.IsNullOrWhiteSpace(language))
        {
            query = query.Where(s => s.Language.ToLower() == language.ToLower());
        }

        var snippets = await query
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        return snippets.Select(MapToDto);
    }

    public async Task<SnippetDto?> GetSnippetByIdAsync(Guid id)
    {
        var snippet = await _context.Snippets.FindAsync(id);
        return snippet == null ? null : MapToDto(snippet);
    }

    public async Task<SnippetDto?> UpdateSnippetAsync(Guid id, UpdateSnippetDto updateDto)
    {
        var snippet = await _context.Snippets.FindAsync(id);
        if (snippet == null)
        {
            return null;
        }

        // Only update properties that are provided
        if (updateDto.Language != null)
            snippet.Language = updateDto.Language;
        
        if (updateDto.Code != null)
            snippet.Code = updateDto.Code;
        
        if (updateDto.Title != null)
            snippet.Title = updateDto.Title;
        
        if (updateDto.Description != null)
            snippet.Description = updateDto.Description;

        snippet.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(snippet);
    }

    public async Task<bool> DeleteSnippetAsync(Guid id)
    {
        var snippet = await _context.Snippets.FindAsync(id);
        if (snippet == null)
        {
            return false;
        }

        _context.Snippets.Remove(snippet);
        await _context.SaveChangesAsync();

        return true;
    }

    // Helper method to map Model to DTO
    private static SnippetDto MapToDto(Snippet snippet)
    {
        return new SnippetDto
        {
            Id = snippet.Id,
            Language = snippet.Language,
            Code = snippet.Code,
            Title = snippet.Title,
            Description = snippet.Description,
            CreatedAt = snippet.CreatedAt,
            UpdatedAt = snippet.UpdatedAt
        };
    }
}