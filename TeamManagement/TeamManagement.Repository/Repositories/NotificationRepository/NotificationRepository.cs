using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel.NotificationModel;

namespace TeamManagement.Repository.Repositories.NotificationRepository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        public NotificationRepository(FUProjectTeamManagementContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateAnNotificationAsync(CreateNotification createNoti)
        {
            DateTime sqlFormattedDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var teacher = await _context.Teachers.Where(te => te.TeacherId == createNoti.TeacherId).FirstOrDefaultAsync();
            var course = await _context.Courses.Where(c => c.CourseId == createNoti.CourseId).FirstOrDefaultAsync();
            if(teacher == null || course == null) return false;
            var noti = new Models.Notification
            {
                TeacherId = teacher.TeacherId,
                CourseId = course.CourseId,
                Title = createNoti.Title,
                FileNoti = createNoti.FileNoti,
                Message = createNoti.Message,
                CreatedDate = sqlFormattedDate,
                Status = 1 
            };
            _context.Notifications.Add(noti);
            await _context.SaveChangesAsync();  
            return true;       
        }
    }
}
