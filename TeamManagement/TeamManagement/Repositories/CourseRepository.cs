using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.Models;

namespace TeamManagement.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FUProjectTeamManagementContext _context;

        public CourseRepository(FUProjectTeamManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses!.ToListAsync();
        }
        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task AddCoursesAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCoursesAsync(Course course)
        {
            var co = _context.Courses.SingleOrDefaultAsync(coo => coo.CourseId == course.CourseId);
            if (co != null)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCoursesAsync(Course course)
        {
            var co = _context.Courses.SingleOrDefaultAsync(coo => coo.CourseId == course.CourseId);
            if (co != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

    }
}
