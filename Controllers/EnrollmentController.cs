using ApiUniversity.Data;
using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversity.Controllers;

[ApiController]
[Route("api/enrollment")]
public class EnrollmentController : ControllerBase
{
    private readonly UniversityContext _context;

    public EnrollmentController(UniversityContext context)
    {
        _context = context;
    }

    // GET: api/course
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetailedEnrollmentDTO>>> GetEnrollments()
    {
        // Get courses and related lists
        var enrollments = _context.Enrollments.Select(x => new DetailedEnrollmentDTO(x));
        return await enrollments.ToListAsync();
    }

    // GET: api/course/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetailedEnrollmentDTO>> GetEnrollment(int id)
    {
        // Find course and related list
        // SingleAsync() throws an exception if no course is found (which is possible, depending on id)
        // SingleOrDefaultAsync() is a safer choice here
        var enrollment = await _context.Enrollments
                                        .Include(s => s.Student)
                                        .Include(c => c.Course)    
                                        .SingleOrDefaultAsync(t => t.Id == id);

        if (enrollment == null)
        {
            return NotFound();
        }

        return new DetailedEnrollmentDTO(enrollment);
    }

    // POST: api/course
    [HttpPost]
    public async Task<ActionResult<DetailedEnrollmentDTO>> PostEnrollment(EnrollmentDTO enrollmentDTO)
    {
        Enrollment enrollment = new(detailedEnrollmentDTO);

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.Id }, new DetailedEnrollmentDTO(enrollment));
    }

    // PUT: api/course/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEnrollment(int id, DetailedEnrollmentDTO detailedEnrollmentDTO)
    {
        if (id != detailedEnrollmentDTO.Id)
        {
            return BadRequest();
        }

        Enrollment enrollment = new(detailedEnrollmentDTO);

        _context.Entry(enrollment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Enrollments.Any(m => m.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/course/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnrollment(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);

        if (enrollment == null)
        {
            return NotFound();
        }

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}