using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.SemesterRepository
{
    public interface ISemesterRepository
    {
        Task<List<SemesterDTO>> GetAllSemesterAsync();
        Task<SemesterDTO> GetSemesterByNameAsync(string semesterName);
        Task<SemesterDTO> GetSemesterByIdAsync(int semesterId);
        Task<bool> CreateSemesterAsync(SemesterDTO semesterDto);
        Task<bool> UpdateASemesterAsync(SemesterDTO semesterDTO);
        Task<bool> DeleteSemesterAsync(int semesterId);
    }
}
