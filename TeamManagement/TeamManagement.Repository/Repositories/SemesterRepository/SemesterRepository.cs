using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
//using TeamManagement.Models;

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
            var semester = await _context.Semesters.Where(s => s.SemName == semesterName).FirstOrDefaultAsync();
            return _mapper.Map<SemesterDTO>(semester);
        }
        public async Task<SemesterDTO> GetSemesterByIdAsync(int semesterId)
        {
            var semester = await _context.Semesters.FindAsync(semesterId);
            return _mapper.Map<SemesterDTO>(semester);
        }
        public async Task<bool> CreateSemesterAsync(SemesterDTO semesterDto)
        {
            var existingSemesterName = _context.Semesters.Where(s => s.SemName == semesterDto.SemName).FirstOrDefault();
            if (existingSemesterName == null)
            {
                var newSemester = _mapper.Map<Semester>(semesterDto);
                newSemester.Status = 1;
                await _context.AddAsync(newSemester);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateASemesterAsync(SemesterDTO semesterDTO)
        {
            if (semesterDTO != null)
            {
                var updateSemester = _mapper.Map<Semester>(semesterDTO);
                _context.Update(updateSemester);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSemesterAsync(int semesterId)
        {
            var semester = await _context.Semesters.FindAsync(semesterId);
            if (semester != null)
            {
                var course = await _context.Courses.Where(c => c.SemId == semesterId).ToListAsync();
                if (course.Any())
                {
                    foreach (var i in course)
                    {
                        i.SemId = null;
                    }
                    await _context.SaveChangesAsync();
                }

                _context.Remove(semester);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
