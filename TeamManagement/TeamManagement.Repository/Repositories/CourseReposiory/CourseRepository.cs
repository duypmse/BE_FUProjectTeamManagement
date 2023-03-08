using AutoMapper;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel.CourseModel;
//using TeamManagement.Models;
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

        public async Task<bool> CreateCoursesAsync(CreateCourseModel courseCM)
        {
            var subj = await _context.Subjects.Where(su => su.SubName.Equals(courseCM.SubName.ToString())).FirstOrDefaultAsync();
            var sem = await _context.Semesters.Where(s => s.SemName.Equals(courseCM.SemName.ToString())).FirstOrDefaultAsync();
            if(subj == null || sem == null) return false;

            var newCourse = new Course
            {
                Image = courseCM.Image,
                CourseName = courseCM.CourseName,   
                KeyEnroll = courseCM.KeyEnroll,
                SubId = subj.SubId,
                SemId = sem.SemId,
                Status = 1
            };
            await _context.Courses.AddAsync(newCourse);
            await _context.SaveChangesAsync();

            var teacher = await _context.Teachers.FindAsync(courseCM.TeacherId);
            var course = await _context.Courses.FindAsync(newCourse.CourseId);
            if (teacher == null || course == null) return false;
            var teacherCourse = new TeacherCourse { TeacherId = teacher.TeacherId, CourseId = course.CourseId };
            await _context.TeacherCourses.AddAsync(teacherCourse);
            await _context.SaveChangesAsync();  

            return true;
        }

        public async Task<bool> UpdateCourseAsync(UpdateCourseModel courseUM)
        {
            var course = await _context.Courses.FindAsync(courseUM.CourseId);
            if (course == null) return false;
            var teacherCourse = await _context.TeacherCourses.Where(tc => tc.CourseId == courseUM.CourseId).FirstOrDefaultAsync();
            var sub = await _context.Subjects.Where(su => su.SubName.Equals(courseUM.SubName)).FirstOrDefaultAsync();
            var sem = await _context.Semesters.Where(se => se.SemName.Equals(courseUM.SemName)).FirstOrDefaultAsync();
            if(sub == null || sem == null) return false;

            course.CourseName = courseUM.CourseName;
            course.Image = courseUM.Image;
            course.KeyEnroll = courseUM.KeyEnroll;
            course.SubId = sub.SubId;
            course.SemId = sem.SemId;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            if (teacherCourse.TeacherId != courseUM.TeacherId)
            {
                _context.TeacherCourses.Remove(teacherCourse);

                var newTeacherCourse = new TeacherCourse
                {
                    TeacherId = courseUM.TeacherId,
                    CourseId = courseUM.CourseId,
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
            return _mapper.Map<List<TeamDTO>>(listTeam);
        }

        public async Task<List<StudentDTO>> GetListStudentNonTeamByCourseIdAsync(int courseId)
        {
            var listStudent = await _context.Participants.Where(c => c.CourseId == courseId && c.TeamId == null).Select(s => s.Stu).ToListAsync();
            return _mapper.Map<List<StudentDTO>>(listStudent);
        }


        public async Task<List<GetCourseHaveTeacher>> GetlistCourseHaveTeacherAsync()
        {
            var result = await (from c in _context.Courses
                                join tc in _context.TeacherCourses on c.CourseId equals tc.CourseId
                                join t in _context.Teachers on tc.TeacherId equals t.TeacherId
                                join su in _context.Subjects on c.SubId equals su.SubId
                                join se in _context.Semesters on c.SemId equals se.SemId
                                select new GetCourseHaveTeacher
                                {
                                    CourseId = c.CourseId,
                                    Image = c.Image,
                                    CourseName = c.CourseName,
                                    KeyEnroll = c.KeyEnroll,
                                    TeacherName = t.TeacherName,
                                    SubName = su.SubName,
                                    SemName = se.SemName,
                                    Status = c.Status
                                }).ToListAsync();
            return result;
        }
    }
}
