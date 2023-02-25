using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Repositories.SubjectRepository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;

        public SubjectRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SubjectDTO>> GetAllSubjectAsync()
        {
            var listSubject = await _context.Subjects.ToListAsync();
            return _mapper.Map<List<SubjectDTO>>(listSubject);
        }

        public async Task<SubjectDTO> GetSubjectByIdAsync(int subjectId)
        {
            var subject = await _context.Subjects.Where(s => s.SubId == subjectId).SingleOrDefaultAsync();
            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task<bool> CreateASubjectAsync(SubjectDTO subjectDTO)
        {
            var sub = _mapper.Map<Subject>(subjectDTO);
            var existingSubject = await _context.Subjects.Where(s => s.SubName == subjectDTO.SubName).FirstOrDefaultAsync();
            if(sub != null && existingSubject == null)
            {
                sub.Status = 1;
                await _context.AddAsync(sub);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }      
    }
}
