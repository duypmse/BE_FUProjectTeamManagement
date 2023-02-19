using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task<List<StudentDTO>> GetAllStudent();
        Task<StudentDTO> StudentDTOFindById(int id);
        Task<StudentDTO> StudentFindByEmailAsync(string email);
    }
}
