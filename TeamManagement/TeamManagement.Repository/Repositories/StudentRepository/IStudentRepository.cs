using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel;
using TeamManagement.Repository.RequestBodyModel.CourseModel;
using TeamManagement.Repository.RequestBodyModel.NotificationModel;
using TeamManagement.Repository.RequestBodyModel.StudentModel;

namespace TeamManagement.Repositories.StudentRepository
{
    public interface IStudentRepository
    {
        Task<List<StudentDTO>> GetAllStudent();
        Task<StudentDTO> GetAStudentById(int studentId);
        Task<StudentDetail> GetAStudentDetailById(int teamId, int studentId);
        Task<List<ActiveCourseModel>> GetAllActiveCoursesForStudentAsync(int studentId);
        Task<List<ActiveCourseWithScoreModel>> GetListCourseByStudentAsync(int studentId);
        Task<List<TeamCourseModel>> GetListTeamByStudentAsync(int studentId);
        Task<List<GetAnNotiByStudent>> GetListNotiByStudentAsync(int courseId, int studentId);
        Task<string> GetStudentNoteAsync(int teamId, int studentId);
        Task<StudentDTO> StudentFindByEmailAsync(string email);
        Task<bool> StudentJoinCourseAsync(JoinCourseModel joinCourseModel);
        Task<bool> StudentOutCourseAsync(JoinCourseModel joinCourseModel);
        Task<bool> AddNoteToStudentAsync(AddNote addNote);
        Task<bool> AddScoreToStudentAsync(AddScore addScore);
        Task<bool> CreateAStudentAsync(StudentDTO studentDTO);
        Task<bool> UpdateAStudentAsync(StudentDTO studentDTO);
        Task<bool> DeleteAStudentAsync(int studentId);
    }
}
