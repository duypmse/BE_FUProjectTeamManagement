﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

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

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            var te = _context.Teachers.SingleOrDefaultAsync(t => t.TeacherId == teacher.TeacherId);
            if (te != null)
            {
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var te = _context.Teachers.Where(t => t.TeacherId == id).FirstOrDefault();
            if (te != null)
            {
                _context.Teachers.Remove(te);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CourseDTO>> GetListCourseByTeacherIdAsync(int teacherId)
        {
            var listCourse = await _context.TeacherCourses.Where(t => t.TeacherId == teacherId)
                                                          .Select(c => c.Course)
                                                          .Where(s => s.Status == 1)
                                                          .ToListAsync();
            return _mapper.Map<List<CourseDTO>>(listCourse);
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
}