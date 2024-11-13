namespace ApiUniversity.Models;
//namespace = libraire interne
//donc ce fichier là et les autres appartiennent à ApiUnviersity.Models

public class Student
{
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
    public string Email{ get; set; } = null!;

    // Default constructor
    public Student() { }

    public Student(StudentDTO studentDTO)
    {
        Id = studentDTO.Id;
        LastName = studentDTO.LastName;
        FirstName = studentDTO.FirstName;
        EnrollmentDate = studentDTO.EnrollmentDate;
        Email=studentDTO.Email;
    }
}