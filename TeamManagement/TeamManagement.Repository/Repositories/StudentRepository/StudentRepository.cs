using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel;
using TeamManagement.RequestBodyModel;
//using TeamManagement.Models;

namespace TeamManagement.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMapper _mapper;
        private readonly FUProjectTeamManagementContext _context;
        public StudentRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<StudentDTO>> GetAllStudent()
        {
            var allStudent = await _context.Students.ToListAsync();
            return _mapper.Map<List<StudentDTO>>(allStudent);
        }

        public async Task<StudentDTO> StudentFindByEmailAsync(string email)
        {
            var student = await _context.Students.Where(s => s.StuEmail == email).FirstOrDefaultAsync();
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<StudentDTO> GetAStudentById(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            return _mapper.Map<StudentDTO>(student);
        }

        public async Task<List<TeamCourseModel>> GetListTeamByStudentAsync(int studentId)
        {
            var listTeam = await (from st in _context.Students
                                  join p in _context.Participants on st.StuId equals p.StuId
                                  join t in _context.Teams on p.TeamId equals t.TeamId
                                  join c in _context.Courses on p.CourseId equals c.CourseId
                                  join tpt in _context.TeamTopics on t.TeamId equals tpt.TeamId
                                  join tp in _context.Topics on tpt.TopicId equals tp.TopicId
                                  where c.Status == 1 && p.StuId == studentId
                                  select new TeamCourseModel
                                  {
                                      TeamId = t.TeamId,
                                      CourseName = c.CourseName,
                                      TeamName = t.TeamName,
                                      TeamCount = t.TeamCount,
                                      TopicName = tp.TopicName
                                  }).ToListAsync();
            return listTeam;
        }

        public async Task<List<ActiveCourseModel>> GetAllActiveCoursesForStudentAsync(int studentId)
        {
            var par = await _context.Participants.Where(p => p.StuId == studentId).Select(p => p.CourseId).ToListAsync();
            var listCourseEnrolled = new List<ActiveCourseModel>();
            if (par.Any())
            {
                listCourseEnrolled = await (from c in _context.Courses
                                            join tc in _context.TeacherCourses on c.CourseId equals tc.CourseId
                                            join t in _context.Teachers on tc.TeacherId equals t.TeacherId
                                            where c.Status == 1 && par.Contains(c.CourseId)
                                            select new ActiveCourseModel
                                            {
                                                CourseId = c.CourseId,
                                                Image = c.Image,
                                                CourseName = c.CourseName,
                                                TeacherName = t.TeacherName,
                                                IsEnrolled = true,
                                            }).ToListAsync();
            }
            var listCourseUnEnrolled = await (from c in _context.Courses
                                              join tc in _context.TeacherCourses on c.CourseId equals tc.CourseId
                                              join t in _context.Teachers on tc.TeacherId equals t.TeacherId
                                              where c.Status == 1 && !par.Contains(c.CourseId)
                                              select new ActiveCourseModel
                                              {
                                                  CourseId = c.CourseId,
                                                  Image = c.Image,
                                                  CourseName = c.CourseName,
                                                  TeacherName = t.TeacherName,
                                                  IsEnrolled = false,
                                              }).ToListAsync();
            listCourseEnrolled.AddRange(listCourseUnEnrolled);
            return listCourseEnrolled;
        }
        public async Task<bool> CreateAStudentAsync(StudentDTO studentDTO)
        {
            var existingEmail = _context.Students.Where(e => e.StuEmail == studentDTO.StuEmail).FirstOrDefault();
            if (existingEmail == null)
            {
                var student = _mapper.Map<Student>(studentDTO);
                student.Status = 1;
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAStudentAsync(StudentDTO studentDTO)
        {
            if (studentDTO != null)
            {
                var updateStudent = _mapper.Map<Student>(studentDTO);
                _context.Students.Update(updateStudent);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAStudentAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                var participant = await _context.Participants.Where(p => p.StuId == studentId).ToListAsync();
                if (participant != null)
                {
                    _context.Participants.RemoveRange(participant);
                }
                _context.Remove(student);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ActiveCourseModel>> GetListCourseByStudentAsync(int studentId)
        {
            //var par = await _context.Participants.Where(p => p.StuId == studentId).Select(c => c.Course).ToListAsync();

            var result = (from p in _context.Participants
                          join c in _context.Courses on p.CourseId equals c.CourseId
                          join te in _context.TeacherCourses on p.CourseId equals te.CourseId
                          join t in _context.Teachers on te.TeacherId equals t.TeacherId
                          where c.Status == 1
                          select new ActiveCourseModel
                          {
                              CourseId = c.CourseId,   
                              Image = c.Image,
                              CourseName = c.CourseName,
                              TeacherName = t.TeacherName,
                              IsEnrolled = true
                          }).Distinct().ToListAsync();
            return await result;
        }
        public async Task<bool> StudentJoinCourseAsync(JoinCourseModel joinCourseModel)
        {
            var courseId = joinCourseModel.CourseId;
            var keyEnroll = joinCourseModel.KeyEnroll;
            var studentId = joinCourseModel.StuId;
            var course = await _context.Courses.Where(c => c.CourseId == courseId && c.KeyEnroll == keyEnroll)
                                               .FirstOrDefaultAsync();
            var student = await _context.Students.Where(s => s.StuId == studentId).FirstOrDefaultAsync();
            var par = await _context.Participants.Where(p => p.StuId == studentId && p.CourseId == courseId).FirstOrDefaultAsync();
            if (course != null && student != null && par == null)
            {
                var newP = new Participant()
                {
                    Course = course,
                    Stu = student,
                    Status = 1
                };
                await _context.AddAsync(newP);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> StudentOutCourseAsync(JoinCourseModel joinCourseModel)
        {
            var courseId = joinCourseModel.CourseId;
            var studentId = joinCourseModel.StuId;
            var listPar = await _context.Participants.Where(p => p.CourseId == courseId && p.StuId == studentId).SingleOrDefaultAsync();
            if (listPar != null)
            {
                if (listPar.TeamId != null)
                {
                    var team = await _context.Teams.Where(t => t.TeamId == listPar.TeamId).FirstOrDefaultAsync();
                    team.TeamCount--;
                }
                _context.Participants.RemoveRange(listPar);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
