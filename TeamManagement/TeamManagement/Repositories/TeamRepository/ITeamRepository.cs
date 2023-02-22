using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repositories.TeamRepository
{
    public interface ITeamRepository
    {
        Task<List<TeamDTO>> GetAllTeamAsync();
        Task<List<StudentDTO>> GetListStudentByTeamIdAsync(int teamId);
        Task AddStudentToTeamAsync(int teamId, int studentId);
    }
}
