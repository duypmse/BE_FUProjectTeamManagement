﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel.CourseModel;
using TeamManagement.Repository.RequestBodyModel;
using TeamManagement.RequestBodyModel;
using TeamManagement.Repository.RequestBodyModel.StudentModel;

namespace TeamManagement.Repositories.CourseReposiory
{
    public interface ICourseRepository
    {
        Task<List<CourseDTO>> GetAllCoursesAsync();
        Task<List<CourseDTO>> GetAllActiveCoursesAsync();
        Task<CourseDTO> GetCourseByIdAsync(int id);
        Task<CourseDTO> GetCourseByNameAsync(string courseName);
        Task<List<GetCourseHaveTeacher>> GetlistCourseHaveTeacherAsync();
        Task<List<TeamDTO>> GetListTeamByCourseIdAsync(int courseId);
        Task<List<StudentDetail>> GetListStudentNonTeamByCourseIdAsync(int courseId);
        //Task<List<CourseDTO>> GetCoursesNotTaughtAsync();
        Task<bool> CreateCoursesAsync(CreateCourseModel courseCM);
        Task<bool> UpdateCourseAsync(UpdateCourseModel courseUM);
        Task<bool> ChangeCourseStatusAsync(int courseId);
    }
}
