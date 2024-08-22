using MinimalAPIProject.Dto.Request;
using MinimalAPIProject.Model;

namespace MinimalAPIProject.Service;
public interface IStudentService
{
    public Task<IEnumerable<Student>> GetAllStudents();
    public Task<Student?> GetStudentById(Guid id);
    public Task<Student> CreateStudent(CreateStudentRequestDto createStudentRequestDto);
    public Task<bool?> UpdateStudent(Guid id, UpdateStudentRequestDto updateStudentRequestDto);
    public Task<bool> DeleteStudent(Guid id);
}