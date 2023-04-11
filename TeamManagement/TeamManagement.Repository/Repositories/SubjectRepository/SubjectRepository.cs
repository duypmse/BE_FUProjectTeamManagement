using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel.CourseModel;
using TeamManagement.Repository.RequestBodyModel.SubjectModel;
using TeamManagement.Repository.RequestBodyModel.TopicModel;
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
                                      SubFullName = su.SubFullName,
                                      Image = su.Image,
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
                SubFullName = newSub.SubFullName,
                Image = newSub.Image,
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
            sub.SubFullName = updateSub.SubFullName;
            sub.Image = updateSub.Image;
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

        public Task<List<CourseDTO>> GetListCourseBySubId(int teacherId, int subId)
        {
            var list = (from su in _context.Subjects
                        join co in _context.Courses on su.SubId equals co.SubId
                        join tc in _context.TeacherCourses on co.CourseId equals tc.CourseId
                        join te in _context.Teachers on tc.TeacherId equals te.TeacherId
                        where su.SubId == subId && co.Status == 1 && te.TeacherId == teacherId
                        select new CourseDTO
                        {
                            CourseId = co.CourseId,
                            Image = co.Image,
                            CourseName = co.CourseName,
                            KeyEnroll = co.KeyEnroll,
                            SubId = co.SubId,
                            SemId = co.SemId,
                            Status = co.Status
                        }).ToListAsync();
            return list;
        }

        public async Task<List<ViewTopic>> GetListTopicBySubIdAsync(int subId)
        {
            var list = await (from su in _context.Subjects
                              join to in _context.Topics on su.SubId equals to.SubId
                              where su.SubId == subId
                              select new ViewTopic
                              {
                                  TopicId = to.TopicId,
                                  TopicName = to.TopicName,
                              }).ToListAsync();
            return list;
        }

        public async Task<List<ViewTopic>> GetListTopicNonTeamInACourse(int subId)
        {
            return await (from su in _context.Subjects
                          join co in _context.Courses on su.SubId equals co.SubId
                          join to in _context.Topics on su.SubId equals to.SubId
                          where su.SubId == subId && co.Status == 1 && to.CourseId == null &&
                                !_context.TeamTopics.Any(tt => tt.TopicId == to.TopicId)
                          select new ViewTopic
                          {
                              TopicId = to.TopicId,
                              TopicName = to.TopicName,
                          }).Distinct().ToListAsync();
        }
    }
}
