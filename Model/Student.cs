namespace MinimalAPIProject.Model;
public class Student
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }
}