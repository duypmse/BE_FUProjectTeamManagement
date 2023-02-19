using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMapper _mapper;
        private readonly FUProjectTeamManagementContext _context;
        public StudentRepository(FUProjectTeamManagementContext context ,IMapper mapper)
        {
            _context= context;
            _mapper = mapper;
        }
        public async Task<List<StudentDTO>> GetAllStudent()
        {
            var allStudent = await _context.Students.ToListAsync();
            return _mapper.Map<List<StudentDTO>>(allStudent);
        }

        public async Task<StudentDTO> StudentFindByEmailAsync(string email)
        {
            var student = await _context.Students.Where(s => s.StuEmail== email).FirstOrDefaultAsync();
            return _mapper.Map<StudentDTO>(student);
        }

        public Task<StudentDTO> StudentDTOFindById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
