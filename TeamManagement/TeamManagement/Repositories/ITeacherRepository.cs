using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repository
{
    public interface ITeacherRepository
    {
        Task<List<TeacherDTO>> GetAllTeacherAsync();
        Task<TeacherDTO> GetTeacherByIdAsync(int id);
        Task<TeacherDTO> GetTeacherByEmailAsync(string email);
        Task AddTeacherAsync(TeacherDTO teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
    }
}
