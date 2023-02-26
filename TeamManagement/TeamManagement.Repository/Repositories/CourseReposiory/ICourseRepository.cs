﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Repositories.CourseReposiory
{
    public interface ICourseRepository
    {
        Task<List<CourseDTO>> GetAllCoursesAsync();
        Task<CourseDTO> GetCourseByIdAsync(int id);
        Task<CourseDTO> GetCourseByNameAsync(string courseName);
        Task<List<TeacherCourseModel>> GetlistCourseHaveTeacherAsync();
        Task<List<TeamDTO>> GetListTeamByCourseIdAsync(int courseId);
        Task<List<StudentDTO>> GetListStudentNonTeamByCourseIdAsync(int courseId);
        Task<List<CourseDTO>> GetCoursesNotTaughtAsync();
        Task<bool> CreateCoursesAsync(TeacherCourseModel TCModel);
        Task<bool> StudentJoinCourse(int courseId, string keyEnroll, int studentId);
        Task UpdateCoursesAsync(Course course);
        Task DeleteCoursesAsync(int id);
    }
}