using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel.SubjectModel;

namespace TeamManagement.Repositories.SubjectRepository
{
    public interface ISubjectRepository
    {
        Task<List<ViewSubject>> GetAllSubjectAsync();
        Task<SubjectDTO> GetSubjectByIdAsync(int subjectId);
        Task<bool> CreateASubjectAsync(CreateSubject newSub);
        Task<bool> UpdateASubjectAsync(UpdateSubject updateSub);
        Task<bool> DeleteASubjectAsync(int subjectId);
    }
}
