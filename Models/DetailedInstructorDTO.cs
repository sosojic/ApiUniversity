using ApiUniversity.Models;

public class DetailedInstructorDTO{
    public int Id{ get; set; }
    public string LastName{ get; set; } = null!;
    public string FirstName{ get; set; } = null!;


     public DetailedInstructorDTO() { }

     public DetailedInstructorDTO(Instructor instructor)
    {
        Id=instructor.Id;
        LastName=instructor.LastName;
        FirstName=instructor.FirstName;
    } 
}