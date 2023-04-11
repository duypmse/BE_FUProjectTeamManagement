using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel.CourseModel;
using TeamManagement.Repository.RequestBodyModel.SubjectModel;
using TeamManagement.Repository.RequestBodyModel.TopicModel;

namespace TeamManagement.Repositories.SubjectRepository
{
    public interface ISubjectRepository
    {
        Task<List<ViewSubject>> GetAllSubjectAsync();
        Task<SubjectDTO> GetSubjectByIdAsync(int subjectId);
        Task<List<ViewTopic>> GetListTopicNonTeamInACourse(int subId);
        Task<List<CourseDTO>> GetListCourseBySubId(int teacherId, int subId);
        Task<List<ViewTopic>> GetListTopicBySubIdAsync(int subId);
        Task<bool> CreateASubjectAsync(CreateSubject newSub);
        Task<bool> UpdateASubjectAsync(UpdateSubject updateSub);
        Task<bool> DeleteASubjectAsync(int subjectId);
    }
}
