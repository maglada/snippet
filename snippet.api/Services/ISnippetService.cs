using snippet.api.DTOs;

namespace snippet.api.Services;

public interface ISnippetService
{
    Task<SnippetDto> CreateSnippetAsync(CreateSnippetDto createDto);
    Task<IEnumerable<SnippetDto>> GetSnippetsAsync(string? language = null);
    Task<SnippetDto?> GetSnippetByIdAsync(Guid id);
    Task<SnippetDto?> UpdateSnippetAsync(Guid id, UpdateSnippetDto updateDto);
    Task<bool> DeleteSnippetAsync(Guid id);
}