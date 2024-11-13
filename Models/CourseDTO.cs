namespace ApiUniversity.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class CourseDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Credits { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public List<Instructor> Instructors { get; set; } = new();

    public CourseDTO() { }

    public CourseDTO(Course course)
    {
        Id = course.Id;
        Title = course.Title;
        Credits = course.Credits;
        DepartmentId= course.DepartmentId;
        Department = course.Department;
    }
}