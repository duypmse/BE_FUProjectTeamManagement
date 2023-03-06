using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
//using TeamManagement.Models;

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
            if (sub != null && existingSubject == null)
            {
                sub.Status = 1;
                await _context.AddAsync(sub);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateASubjectAsync(SubjectDTO subjectDTO)
        {
            if (subjectDTO != null)
            {
                var updateSubject = _mapper.Map<Subject>(subjectDTO);
                _context.Update(updateSubject);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteASubjectAsync(int subjectId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject != null)
            {
                var isInCourse = await _context.Courses.Where(c => c.SubId == subjectId).ToListAsync();
                if (isInCourse.Any())
                {
                    foreach (var c in isInCourse)
                    {
                        c.SubId = null;
                    }
                    await _context.SaveChangesAsync();
                }
                _context.Remove(subject);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
