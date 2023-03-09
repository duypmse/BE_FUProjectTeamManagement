using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel.SubjectModel;
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

        public async Task<List<ViewSubject>> GetAllSubjectAsync()
        {
            var subjects = await (from su in _context.Subjects
                                  join de in _context.Departments on su.DeptId equals de.DeptId
                                  select new ViewSubject
                                  {
                                      SubId = su.SubId,
                                      SubName = su.SubName,
                                      DeptName = de.DeptName,
                                      Status = su.Status,
                                  }).ToListAsync();
            return subjects;
        }

        public async Task<SubjectDTO> GetSubjectByIdAsync(int subjectId)
        {
            var subject = await _context.Subjects.Where(s => s.SubId == subjectId).SingleOrDefaultAsync();
            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task<bool> CreateASubjectAsync(CreateSubject newSub)
        {
            var dep = await _context.Departments.Where(de => de.DeptName == newSub.DeptName.ToUpper()).FirstOrDefaultAsync();
            var existingSub = await _context.Subjects.Where(su => su.SubName == newSub.SubName.ToUpper()).FirstOrDefaultAsync();
            if (dep == null || existingSub != null) return false;
            var newSubject = new Subject
            {
                SubName = newSub.SubName.ToUpper(),
                DeptId = dep.DeptId,
                Status = 1
            };
            _context.Subjects.Add(newSubject);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateASubjectAsync(UpdateSubject updateSub)
        {
            var sub = await _context.Subjects.FindAsync(updateSub.SubId);
            var dep = await _context.Departments.Where(de => de.DeptName == updateSub.DeptName.ToUpper()).FirstOrDefaultAsync();
            var existingSub = await _context.Subjects.Where(su => su.SubName == updateSub.SubName && su.SubId != updateSub.SubId).FirstOrDefaultAsync();
            if (sub == null || dep == null || existingSub != null) return false;

            sub.SubName = updateSub.SubName.ToUpper();
            sub.DeptId = dep.DeptId;

            _context.Subjects.Update(sub);  
            await _context.SaveChangesAsync();
            return true;
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
