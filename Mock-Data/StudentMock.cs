using MinimalAPIProject.Model;

namespace MinimalAPIProject.Mock_Data;
public class StudentMock
{
    public static IEnumerable<Student> students = new List<Student>(){
        new Student() { Name = "Test 1", Age = new Random().Next(18, 60), Id = Guid.NewGuid() },
        new Student() { Name = "Test 2", Age = new Random().Next(18, 60), Id = Guid.NewGuid() },
        new Student() { Name = "Test 3", Age = new Random().Next(18, 60), Id = Guid.NewGuid() },
        new Student() { Name = "Test 4", Age = new Random().Next(18, 60), Id = Guid.NewGuid() },
        new Student() { Name = "Test 5", Age = new Random().Next(18, 60), Id = Guid.NewGuid() }
    }; 
}