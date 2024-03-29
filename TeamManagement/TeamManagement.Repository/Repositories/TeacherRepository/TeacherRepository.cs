﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel.NotificationModel;
using TeamManagement.Repository.RequestBodyModel.SubjectModel;

namespace TeamManagement.Repositories.TeacherRepository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;

        public TeacherRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TeacherDTO>> GetAllTeacherAsync()
        {
            var allTeacher = await _context.Teachers.ToListAsync();
            return _mapper.Map<List<TeacherDTO>>(allTeacher);
        }

        public async Task<TeacherDTO> GetTeacherByIdAsync(int id)
        {
            var teacher = await _context.Teachers!.Where(t => t.TeacherId == id).FirstOrDefaultAsync();
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<TeacherDTO> GetTeacherByNameAsync(string teacherName)
        {
            var teacher = await _context.Teachers.Where(t => t.TeacherName == teacherName).FirstOrDefaultAsync();
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<TeacherDTO> GetTeacherByEmailAsync(string email)
        {
            var teacher = await _context.Teachers.Where(t => t.TeacherEmail == email).FirstOrDefaultAsync();
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<bool> CreateTeacherAsync(TeacherDTO teacherDto)
        {
            var existingEmail = _context.Teachers.Where(e => e.TeacherEmail == teacherDto.TeacherEmail).FirstOrDefault();
            if (existingEmail == null)
            {
                var newTeacher = _mapper.Map<Teacher>(teacherDto);
                newTeacher.Status = 1;
                await _context.Teachers.AddAsync(newTeacher);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTeacherAsync(TeacherDTO teacherDTO)
        {
            if (teacherDTO != null)
            {
                var updateTeacher = _mapper.Map<Teacher>(teacherDTO);
                _context.Teachers.Update(updateTeacher);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTeacherAsync(int teacherId)
        {
            var te = await _context.Teachers.FindAsync(teacherId);
            var teacherTeam = await _context.TeacherTeams.Where(tt => tt.TeacherId == teacherId).ToListAsync();
            var teacherTopic = await _context.TeacherTopics.Where(ttp => ttp.TeacherId == teacherId).ToListAsync();
            var teacherCourse = await _context.TeacherCourses.Where(tc => tc.TeacherId == teacherId).ToListAsync();
            if (te != null)
            {
                if (teacherCourse != null)
                {
                    _context.TeacherCourses.RemoveRange(teacherCourse);
                }
                if (teacherTopic != null)
                {
                    _context.TeacherTopics.RemoveRange(teacherTopic);
                }
                if (teacherTeam != null)
                {
                    _context.TeacherTeams.RemoveRange(teacherTeam);
                }
                _context.Teachers.Remove(te);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CourseDTO>> GetListCourseByTeacherIdAsync(int teacherId)
        {
            var listCourse = await _context.TeacherCourses.Where(t => t.TeacherId == teacherId)
                                                          .Select(c => c.Course)
                                                          .Where(s => s.Status == 1)
                                                          .ToListAsync();
            return _mapper.Map<List<CourseDTO>>(listCourse);
        }

        public async Task<List<GetAnNotification>?> GetListNotificationByTeacherAcync(int teacherId, int courseId)
        {
            var course = await _context.Courses.Where(co => co.CourseId == courseId && co.Status == 1).FirstOrDefaultAsync();
            var isTeacherInCourse = await _context.TeacherCourses
                                .Where(tc => tc.TeacherId == teacherId && tc.CourseId == courseId)
                                .FirstOrDefaultAsync();
            if (course != null && isTeacherInCourse != null)
            {
                var listNoti = await (from n in _context.Notifications
                                      where n.CourseId == courseId && n.TeacherId == teacherId
                                      select new GetAnNotification
                                      {
                                          NotificationId = n.NotificationId,
                                          Title = n.Title,
                                          CreatedDate = n.CreatedDate,
                                          FileNoti = n.FileNoti,
                                          Message = n.Message,
                                      }).ToListAsync();
                return listNoti;
            }
            return null;
        }

        public async Task<List<GetSubject>> GetListSubjectByTeacherIdAsync(int teacherId)
        {
            var listSub = await (from su in _context.Subjects
                                 join de in _context.Departments on su.DeptId equals de.DeptId
                                 join c in _context.Courses on su.SubId equals c.SubId
                                 join tc in _context.TeacherCourses on c.CourseId equals tc.CourseId
                                 join te in _context.Teachers on tc.TeacherId equals te.TeacherId
                                 where te.TeacherId == teacherId && c.Status == 1
                                 select new GetSubject
                                 {
                                     SubId = su.SubId,
                                     SubName = su.SubName,
                                     SubFullName = su.SubFullName,
                                     Image = su.Image,
                                     DeptName = de.DeptName,
                                     Status = su.Status,
                                 }).Distinct().ToListAsync();
            return listSub;

        }
    }
    //public async Task<bool> AddCoursesToTeacherAsync(int teacherId, List<int> courseIds)
    //{
    //    var teacher = await _context.Teachers.FindAsync(teacherId);
    //    if (teacher == null) return false;
    //    var courses = await _context.Courses.Where(c => courseIds.Contains(c.CourseId)).ToListAsync();
    //    if (courses.Any())
    //    {
    //        var teacherCourses = courses.Select(c => new TeacherCourse { TeacherId = teacherId, CourseId = c.CourseId });
    //        await _context.TeacherCourses.AddRangeAsync(teacherCourses);
    //        await _context.SaveChangesAsync();
    //        return true;
    //    }
    //    return false;
    //}

}
