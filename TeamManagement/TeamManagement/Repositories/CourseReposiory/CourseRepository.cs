﻿using AutoMapper;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

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
            var allCourse = await _context.Courses!.ToListAsync();
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
        public async Task<bool> CreateCoursesAsync(CourseDTO courseDto)
        {
            var newCourse = _mapper.Map<Course>(courseDto);
            if ( courseDto != null)
            {
                newCourse.Status = 1;
                await _context.Courses.AddAsync(newCourse);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;     
        }
        public async Task UpdateCoursesAsync(Course course)
        {
            var co = _context.Courses.SingleOrDefaultAsync(c => c.CourseId == course.CourseId);
            if (co != null)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCoursesAsync(int id)
        {
            var co = _context.Courses.Where(c => c.CourseId == id).FirstOrDefault();
            if (co != null)
            {
                _context.Courses.Remove(co);
                await _context.SaveChangesAsync();
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

        public async Task<bool> StudentJoinCourse(int courseId, string keyEnroll, int studentId)
        {
            var course = await _context.Courses.Where(c => c.CourseId == courseId && c.KeyEnroll == keyEnroll)
                                               .FirstOrDefaultAsync();
            var student = await _context.Students.Where(s => s.StuId == studentId).FirstOrDefaultAsync();
            var par = await _context.Participants.Where(p => p.StuId == studentId).FirstOrDefaultAsync();
            if(course != null && student != null && par == null)
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
    }
}
