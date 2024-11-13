namespace ApiUniversity.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int InstructorId{ get; set; }
    public Instructor Administrator{ get; set; } =null!;
    public List<Course> Courses { get; set; } = new();
    

    public Department() { }

   
}