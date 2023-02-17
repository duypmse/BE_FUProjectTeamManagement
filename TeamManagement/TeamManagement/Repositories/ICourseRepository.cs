using System.Collections.Generic;
using System.Threading.Tasks;
using TeamManagement.Models;

namespace TeamManagement.Repository
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCoursesAsync(Course course);
        Task UpdateCoursesAsync(Course course);
        Task DeleteCoursesAsync(Course course);
    }
}
