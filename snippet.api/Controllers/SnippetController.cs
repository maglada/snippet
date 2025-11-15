using Microsoft.AspNetCore.Mvc;
using snippet.api.DTOs;
using snippet.api.Models;
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
        var SnippetDto = await _snippetService.CreateSnippetAsync(createDto);
        return CreatedAtAction(nameof(GetSnippet), new { id = SnippetDto.Id }, SnippetDto);
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
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid snippet ID.");
        }

        var SnippetDto = await _snippetService.GetSnippetByIdAsync(id);
        if (SnippetDto == null)
        {
            return NotFound();
        }
        return Ok(SnippetDto);
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
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid snippet ID.");
        }

        var updatedSnippet = await _snippetService.UpdateSnippetAsync(id, updateDto);
        if (updatedSnippet == null)
        {
            return NotFound();
        }

        return Ok(updatedSnippet);
    }

    /// <summary>
    /// Delete a snippet
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSnippet(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid snippet ID.");
        }

        var deleted = await _snippetService.DeleteSnippetAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}