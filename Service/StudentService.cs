using MinimalAPIProject.Dto.Request;
using MinimalAPIProject.Model;
using MinimalAPIProject.Repository;

namespace MinimalAPIProject.Service;
public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository; 
    public StudentService(IStudentRepository studentRepository){
        _studentRepository = studentRepository;
    }
    public async Task<Student> CreateStudent(CreateStudentRequestDto createStudentRequestDto)
    {
        Student student = new Student
        {
            Id = Guid.NewGuid(),
            Name = createStudentRequestDto.Name,
            Age = createStudentRequestDto.Age
        };
        return await _studentRepository.CreateStudent(student);
    }

    public async Task<bool> DeleteStudent(Guid id)
    {
        return await _studentRepository.DeleteStudent(id);
    }

    public async Task<IEnumerable<Student>> GetAllStudents()
    {
        return await _studentRepository.GetAllStudents();
    }

    public async Task<Student?> GetStudentById(Guid id)
    {
        return await _studentRepository.GetStudentById(id);
    }

    public async Task<bool?> UpdateStudent(Guid id, UpdateStudentRequestDto updateStudentRequestDto)
    {
        Student student = new Student
        {
            Id = id,
            Name = updateStudentRequestDto.Name,
            Age = updateStudentRequestDto.Age
        };
        return await _studentRepository.UpdateStudent(student);
    }
}