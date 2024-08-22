using MinimalAPIProject.Model;

namespace MinimalAPIProject.Repository;
public interface IStudentRepository
{
    public Task<IEnumerable<Student>> GetAllStudents();
    public Task<Student?> GetStudentById(Guid id);
    public Task<Student> CreateStudent(Student student);
    public Task<bool?> UpdateStudent(Student student);
    public Task<bool> DeleteStudent(Guid id);
}