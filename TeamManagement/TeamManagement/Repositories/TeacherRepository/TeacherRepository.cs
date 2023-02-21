using AutoMapper;
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

        public async Task<TeacherDTO> GetTeacherByEmailAsync(string email)
        {
            var teacher = await _context.Teachers.Where(t => t.TeacherEmail == email).FirstOrDefaultAsync();
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task AddTeacherAsync(TeacherDTO teacherDto)
        {
            var newTeacher = _mapper.Map<Teacher>(teacherDto);
            await _context.Teachers.AddAsync(newTeacher);
            await _context.SaveChangesAsync();
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
                                                          .Where(s => s.Status == 1).ToListAsync();
            return _mapper.Map<List<CourseDTO>>(listCourse);
        }
    }
}
