using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.SemesterRepository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;
        public SemesterRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SemesterDTO>> GetAllSemesterAsync()
        {
            var listSemester = await _context.Semesters.ToListAsync();
            return _mapper.Map<List<SemesterDTO>>(listSemester);
        }

        public async Task<SemesterDTO> GetSemesterByNameAsync(string semesterName)
        {
            var semester = await _context.Semesters.Where(s => s.SemName== semesterName).FirstOrDefaultAsync();
            return _mapper.Map<SemesterDTO>(semester);
        }
        public async Task<bool> CreateSemesterAsync(SemesterDTO semesterDto)
        {
            var existingSemesterName = _context.Semesters.Where(s => s.SemName == semesterDto.SemName).FirstOrDefault();    
            if(existingSemesterName == null)
            {
                var newSemester = _mapper.Map<Semester>(semesterDto);
                newSemester.Status = 1;
                await _context.AddAsync(newSemester);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
