using AutoMapper;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Repositories.CourseReposiory
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CourseDTO>> GetAllCoursesAsync()
        {
            var allCourse = await _context.Courses.ToListAsync();
            return _mapper.Map<List<CourseDTO>>(allCourse);
        }
        public async Task<List<CourseDTO>> GetAllActiveCoursesAsync()
        {
            var allCourse = await _context.Courses.Where(c => c.Status == 1).ToListAsync();
            return _mapper.Map<List<CourseDTO>>(allCourse);
        }
        public async Task<CourseDTO> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.Where(c => c.CourseId == id).FirstOrDefaultAsync();
            return _mapper.Map<CourseDTO>(course);
        }
        public async Task<CourseDTO> GetCourseByNameAsync(string courseName)
        {
            var course = await _context.Courses.Where(c => c.CourseName == courseName).FirstOrDefaultAsync();
            return _mapper.Map<CourseDTO>(course);
        }
        //public async Task<List<CourseDTO>> GetCoursesNotTaughtAsync()
        //{
        //    var isTeach = await _context.Courses.Where(c => !_context.TeacherCourses
        //                                        .Any(tc => tc.CourseId == c.CourseId))
        //                                        .ToListAsync();
        //    return _mapper.Map<List<CourseDTO>>(isTeach);
        //}

        public async Task<bool> CreateCoursesAsync(TeacherCourseModel TCModel)
        {
            var newCourse = _mapper.Map<Course>(TCModel);
            var teacher = await _context.Teachers.FindAsync(TCModel.TeacherId);
            if (newCourse == null || teacher == null) return false;
            newCourse.Status = 1;
            var teacherCourse = new TeacherCourse { TeacherId = TCModel.TeacherId, Course = newCourse };
            await _context.TeacherCourses.AddAsync(teacherCourse);
            await _context.Courses.AddAsync(newCourse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourseAsync(int teacherId, TeacherCourseModel TCModel)
        {
            if (TCModel.CourseId == 0) return false;
            var co = _mapper.Map<Course>(TCModel);
            _context.Courses.Update(co);
            await _context.SaveChangesAsync();

            if(teacherId != TCModel.TeacherId)
            {
                var teacherCourse = await _context.TeacherCourses
                    .Where(tc => tc.TeacherId == teacherId && tc.CourseId == TCModel.CourseId)
                    .FirstOrDefaultAsync();
                _context.TeacherCourses.Remove(teacherCourse);

                var newTeacherCourse = new TeacherCourse
                {
                    TeacherId = TCModel.TeacherId,
                    CourseId = TCModel.CourseId,
                };
                _context.Add(newTeacherCourse);
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> ChangeCourseStatusAsync(int courseId)
        {
            var co = await _context.Courses.Where(c => c.CourseId == courseId).FirstOrDefaultAsync();
            if (co == null) return false;
            if (co.Status == 1)
            {
                co.Status = 0;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                co.Status = 1;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<TeamDTO>> GetListTeamByCourseIdAsync(int courseId)
        {
            var listTeam = await _context.CourseTeams.Where(c => c.CourseId == courseId).Select(t => t.Team).ToListAsync();
            //var teams = new List<TeamDTO>();
            //foreach (var team in listTeam)
            //{
            //    var teamMatch = new TeamDTO
            //    {
            //        TeamId= team.TeamId,
            //        TeamName= team.TeamName.Split('@')[0],
            //        TeamCount= team.TeamCount,
            //        Status= team.Status,
            //    };
            //    teams.Add(teamMatch);
            //}
            return _mapper.Map<List<TeamDTO>>(listTeam);
        }

        public async Task<List<StudentDTO>> GetListStudentNonTeamByCourseIdAsync(int courseId)
        {
            var listStudent = await _context.Participants.Where(c => c.CourseId == courseId && c.TeamId == null).Select(s => s.Stu).ToListAsync();
            return _mapper.Map<List<StudentDTO>>(listStudent);
        }

        public async Task<bool> StudentJoinCourse(int courseId, string keyEnroll, int studentId)
        {
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

        public async Task<List<TeacherCourseModel>> GetlistCourseHaveTeacherAsync()
        {
            var result = from c in _context.Courses
                         join tc in _context.TeacherCourses on c.CourseId equals tc.CourseId
                         join t in _context.Teachers on tc.TeacherId equals t.TeacherId
                         select new TeacherCourseModel
                         {
                             CourseId = c.CourseId,
                             CourseName = c.CourseName,
                             KeyEnroll = c.KeyEnroll,
                             TeacherId = t.TeacherId,
                             TeacherName = t.TeacherName,
                             SubId = c.SubId,
                             SemId = c.SemId,
                             Status = c.Status
                         };
            var list = await result.ToListAsync();
            return _mapper.Map<List<TeacherCourseModel>>(list);
        }
    }
}
