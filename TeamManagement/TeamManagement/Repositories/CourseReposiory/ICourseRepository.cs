﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.CourseReposiory
{
    public interface ICourseRepository
    {
        Task<List<CourseDTO>> GetAllCoursesAsync();
        Task<CourseDTO> GetCourseByIdAsync(int id);
        Task<CourseDTO> GetCourseByNameAsync(string courseName);
        Task<List<TeamDTO>> GetListTeamByCourseIdAsync(int courseId);
        Task<List<StudentDTO>> GetListStudentByCourseIdAsync(int courseId);
        Task AddCoursesAsync(CourseDTO courseDto);
        Task UpdateCoursesAsync(Course course);
        Task DeleteCoursesAsync(int id);
    }
}
