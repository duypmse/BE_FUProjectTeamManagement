﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.RequestBodyModel.NotificationModel;

namespace TeamManagement.Repositories.TeacherRepository
{
    public interface ITeacherRepository
    {
        Task<List<TeacherDTO>> GetAllTeacherAsync();
        Task<TeacherDTO> GetTeacherByIdAsync(int id);
        Task<TeacherDTO> GetTeacherByNameAsync(string teacherName);
        Task<TeacherDTO> GetTeacherByEmailAsync(string email);
        Task<List<CourseDTO>> GetListCourseByTeacherIdAsync(int teacherId);
        Task<List<GetAnNotification>?> GetListNotificationByTeacherAcync(int teacherId, int courseId);
        Task<bool> CreateTeacherAsync(TeacherDTO teacher);
        //Task<bool> AddCoursesToTeacherAsync(int teacherId, List<int> courseIds);
        Task<bool> UpdateTeacherAsync(TeacherDTO teacherDTO);
        Task<bool> DeleteTeacherAsync(int teacherId);
    }
}
