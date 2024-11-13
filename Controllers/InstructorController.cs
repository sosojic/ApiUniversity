using ApiUniversity.Data;
using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversity.Controllers;

[ApiController]
[Route("api/enrollment")]
public class InstructorController : ControllerBase
{
    private readonly UniversityContext _context;

    public InstructorController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/course
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetailedInstructorDTO>>> GetInstructors()
    {
        // Get courses and related lists
        var instructors = _context.Instructors.Select(x => new DetailedInstructorDTO(x));
        return await instructors.ToListAsync();
    }

    // GET: api/course/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedInstructorDTO>> GetInstructors(int id)
    {
        // Find course and related list
        // SingleAsync() throws an exception if no course is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var instructor = await _context.Instructors
                                        .Include(c => c.Courses)
                                        .Include(d => d.AdministeredDepartments)
                                        .SingleOrDefaultAsync(t => t.Id == id);

        if (instructor == null)
        {
            return NotFound();
        }

        return new DetailedInstructorDTO(instructor);
    }

    // POST: api/course
    [HttpPost]
    public async Task<ActionResult<DetailedInstructorDTO>> PostInstructors(InstructorDTO instructorDTO)
    {
        Instructor instructor = new(instructorDTO);

        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();



        return CreatedAtAction(nameof(GetInstructors), new { id = instructor.Id }, new DetailedInstructorDTO(instructor));
    }

    
    
}