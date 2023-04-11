using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel;
using TeamManagement.Repository.RequestBodyModel.StudentModel;
using TeamManagement.Repository.RequestBodyModel.TeamTopicModel;

namespace TeamManagement.Repositories.TeamRepository
{
    public interface ITeamRepository
    {
        Task<List<TeamDTO>> GetAllTeamAsync();
        Task<List<StudentDetail>> GetListStudentByTeamIdAsync(int teamId);
        Task<TopicDTO> GetATopicByTeamIdAsync(int teamId);
        Task<TeamDetailModel?> GetTeamDetailByIdAsync(int teamId);
        Task<bool> AddStudentToTeamAsync(int teamId, List<int> studentIds);
        Task<bool> AddTopicToTeamAsync(AddTopicToTeam addTT);
        Task<bool> CreateATeamToCourseAsync(int courseId, TeamDTO teamDto);
        Task<bool> UpdateATeamAsync(TeamDTO teamDTO);
        Task<bool> RemoveATeamAsync(int teamId);
        Task<bool> RemoveAStudentInTeamAsync(int studentId, int teamId);
    }
}
