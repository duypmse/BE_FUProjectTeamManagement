using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;

namespace TeamManagement.Repositories.SubjectRepository
{
    public interface ISubjectRepository
    {
        Task<List<SubjectDTO>> GetAllSubjectAsync();
        Task<SubjectDTO> GetSubjectByIdAsync(int subjectId);
        Task<bool> CreateASubjectAsync(SubjectDTO subjectDTO);
        Task<bool> UpdateASubjectAsync(SubjectDTO subjectDTO);
        Task<bool> DeleteASubjectAsync(int subjectId);
    }
}
