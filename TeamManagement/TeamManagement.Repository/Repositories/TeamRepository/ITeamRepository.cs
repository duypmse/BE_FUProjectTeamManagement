using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Repositories.TeamRepository
{
    public interface ITeamRepository
    {
        Task<List<TeamDTO>> GetAllTeamAsync();
        Task<List<StudentDTO>> GetListStudentByTeamIdAsync(int teamId);
        Task<bool> AddStudentToTeamAsync(int teamId, List<int> studentIds);
        Task<bool> CreateATeamToCourseAsync(int courseId, TeamDTO teamDto);
        Task<bool> RemoveATeamAsync(int teamId);
        Task<bool> RemoveAStudentInTeamAsync(int studentId, int teamId);
    }
}
