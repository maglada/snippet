using Microsoft.AspNetCore.Mvc;
using snippet.api.DTOs;
using snippet.api.Services;

namespace snippet.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SnippetsController : ControllerBase
{
    private readonly ISnippetService _snippetService;

    public SnippetsController(ISnippetService snippetService)
    {
        _snippetService = snippetService;
    }

    /// <summary>
    /// Create a new snippet
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SnippetDto>> CreateSnippet([FromBody] CreateSnippetDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var snippet = await _snippetService.CreateSnippetAsync(createDto);
        
        return CreatedAtAction(
            nameof(GetSnippet), 
            new { id = snippet.Id }, 
            snippet
        );
    }

    /// <summary>
    /// Get all snippets, optionally filtered by language
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SnippetDto>>> GetSnippets(
        [FromQuery] string? language = null)
    {
        var snippets = await _snippetService.GetSnippetsAsync(language);
        return Ok(snippets);
    }

    /// <summary>
    /// Get a specific snippet by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SnippetDto>> GetSnippet(Guid id)
    {
        var snippet = await _snippetService.GetSnippetByIdAsync(id);
        
        if (snippet == null)
        {
            return NotFound(new { message = $"Snippet with ID {id} not found" });
        }

        return Ok(snippet);
    }

    /// <summary>
    /// Update a snippet
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SnippetDto>> UpdateSnippet(
        Guid id, 
        [FromBody] UpdateSnippetDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var snippet = await _snippetService.UpdateSnippetAsync(id, updateDto);
        
        if (snippet == null)
        {
            return NotFound(new { message = $"Snippet with ID {id} not found" });
        }

        return Ok(snippet);
    }

    /// <summary>
    /// Delete a snippet
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSnippet(Guid id)
    {
        var deleted = await _snippetService.DeleteSnippetAsync(id);
        
        if (!deleted)
        {
            return NotFound(new { message = $"Snippet with ID {id} not found" });
        }

        return NoContent();
    }
}