using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repositories.TeamRepository
{
    public interface ITeamRepository
    {
        Task<List<TeamDTO>> GetAllTeamAsync();
        Task<List<StudentDTO>> GetListStudentByTeamIdAsync(int teamId);
        Task<bool> AddStudentToTeamAsync(int teamId, int studentId);
        Task<bool> CreateATeamToCourseAsync(int courseId, TeamDTO teamDto);
        Task<bool> RemoveATeamAsync(int teamId);
        Task<bool> RemoveAStudentInTeamAsync(int studentId, int teamId);
    }
}
