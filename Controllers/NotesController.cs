using IsLabApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace IsLabApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private static List<Note> _notes = new();
    private static int _nextId = 1;

    [HttpPost]
    public IActionResult Create([FromBody] Note note)
    {
        if (string.IsNullOrWhiteSpace(note.Title))
        {
            return BadRequest(new { error = "Title is required" });
        }

        note.Id = _nextId++;
        note.CreatedAt = DateTime.UtcNow;
        _notes.Add(note);

        return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_notes);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note == null)
        {
            return NotFound();
        }
        return Ok(note);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var note = _notes.FirstOrDefault(n => n.Id == id);
        if (note == null)
        {
            return NotFound();
        }
        _notes.Remove(note);
        return NoContent();
    }
}