namespace MinimalAPIProject.Dto.Request;
public class CreateStudentRequestDto
{
    public required string Name { get; set; }
    public int Age { get; set; }
}