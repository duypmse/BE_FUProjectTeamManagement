﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.TeacherRepository
{
    public interface ITeacherRepository
    {
        Task<List<TeacherDTO>> GetAllTeacherAsync();
        Task<TeacherDTO> GetTeacherByIdAsync(int id);
        Task<TeacherDTO> GetTeacherByNameAsync(string teacherName);
        Task<TeacherDTO> GetTeacherByEmailAsync(string email);
        Task<List<CourseDTO>> GetListCourseByTeacherIdAsync(int teacherId);
        Task<bool> CreateTeacherAsync(TeacherDTO teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
    }
}
