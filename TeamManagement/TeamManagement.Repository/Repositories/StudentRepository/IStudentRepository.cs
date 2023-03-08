using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel;
using TeamManagement.Repository.RequestBodyModel.CourseModel;

namespace TeamManagement.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task<List<StudentDTO>> GetAllStudent();
        Task<StudentDTO> GetAStudentById(int studentId);
        Task<List<ActiveCourseModel>> GetAllActiveCoursesForStudentAsync(int studentId);
        Task<List<ActiveCourseModel>> GetListCourseByStudentAsync(int studentId);
        Task<List<TeamCourseModel>> GetListTeamByStudentAsync(int studentId);
        Task<StudentDTO> StudentFindByEmailAsync(string email);
        Task<bool> StudentJoinCourseAsync(JoinCourseModel joinCourseModel);
        Task<bool> StudentOutCourseAsync(JoinCourseModel joinCourseModel);
        Task<bool> CreateAStudentAsync(StudentDTO studentDTO);
        Task<bool> UpdateAStudentAsync(StudentDTO studentDTO);
        Task<bool> DeleteAStudentAsync(int studentId);
    }
}
