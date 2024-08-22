namespace MinimalAPIProject.Dto.Response;
public class StudentResponseDto
{
    public Guid Id { get; set;}
    public required string Name { get; set; }
    public int Age { get; set; }
}