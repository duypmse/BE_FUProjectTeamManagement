using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel;
using TeamManagement.Repository.RequestBodyModel.StudentModel;
using TeamManagement.Repository.RequestBodyModel.TeamTopicModel;

namespace TeamManagement.Repositories.TeamRepository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly FUProjectTeamManagementContext _context;
        private readonly IMapper _mapper;

        public TeamRepository(FUProjectTeamManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TeamDTO>> GetAllTeamAsync()
        {
            var listTeam = await _context.Teams.ToListAsync();
            return _mapper.Map<List<TeamDTO>>(listTeam);
        }

        public async Task<List<StudentDetail>> GetListStudentByTeamIdAsync(int teamId)
        {
            var listStudent = await (from p in _context.Participants
                                     join s in _context.Students on p.StuId equals s.StuId
                                     where p.TeamId == teamId && p.CourseId != null && p.Status != 0
                                     select new StudentDetail
                                     {
                                         StuId = s.StuId,
                                         StuCode = s.StuCode,
                                         StuName = s.StuName,
                                         StuEmail = s.StuEmail,
                                         StuPhone = s.StuPhone,
                                         DateOfBirth = s.DateOfBirth,
                                         StuGender = s.StuGender,
                                         TeacherNote = p.TeacherNote,
                                         Score = p.Score,
                                         Status = p.Status,
                                     }).ToListAsync();
            return listStudent;
        }
        public async Task<TeamDetailModel?> GetTeamDetailByIdAsync(int teamId)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);
            if (team == null) return null;
            var teamTopic = await _context.TeamTopics.Where(tt => tt.TeamId == teamId).Select(tp => tp.Topic).FirstOrDefaultAsync();

            var listStudent = await _context.Participants
                                    .Where(p => p.TeamId == teamId)
                                    .Select(p => p.Stu)
                                    .ToListAsync();


            if (listStudent == null) return null;
            var teamDetail = new TeamDetailModel
            {
                TeamName = team?.TeamName,
                TeamCount = listStudent.Count,
                Students = _mapper.Map<List<StudentDTO>>(listStudent),
                TopicName = teamTopic?.TopicName,
                DeadlineDate = teamTopic?.DeadlineDate,
                Requirement = teamTopic?.Requirement
            };

            return teamDetail;
        }
        public async Task<TopicDTO> GetATopicByTeamIdAsync(int teamId)
        {
            var topic = await _context.TeamTopics.Where(te => te.TeamId == teamId).Select(to => to.Topic).SingleOrDefaultAsync();
            return _mapper.Map<TopicDTO>(topic);
        }

        public async Task<bool> AddStudentToTeamAsync(int teamId, List<int> studentIds)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var teamCourse = await _context.CourseTeams.Where(ct => ct.TeamId == teamId).Select(c => c.Course).SingleOrDefaultAsync();
            if (team == null || teamCourse == null) return false;
            var participants = await _context.Participants.Where(p => studentIds
                                                          .Contains((int)p.StuId) && p.CourseId == teamCourse.CourseId && p.TeamId == null)
                                                          .ToListAsync();
            if (participants == null) return false;
            foreach (var p in participants)
            {
                p.Team = team;
                team.TeamCount++;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateATeamToCourseAsync(int courseId, TeamDTO teamDto)
        {
            var dateNow = DateTime.Now;
            var course = await _context.Courses.Where(c => c.CourseId == courseId).FirstOrDefaultAsync();
            var teacher = await _context.TeacherCourses.Where(tc => tc.CourseId == courseId).Select(t => t.Teacher).FirstOrDefaultAsync();
            if (course != null)
            {
                var newTeam = _mapper.Map<Team>(teamDto);

                //var newTeamName = newTeam.TeamName + "@" + dateNow;
                //var existingTeamName = await _context.Teams.Where(t => t.TeamName == newTeamName).FirstOrDefaultAsync();
                //if (existingTeamName != null) return false;

                var newCourse = new CourseTeam()
                {
                    Course = course,
                    Team = newTeam
                };
                await _context.AddAsync(newCourse);

                var newTeacherTeam = new TeacherTeam()
                {
                    Teacher = teacher,
                    Team = newTeam
                };
                await _context.AddAsync(newTeacherTeam);

                //newTeam.TeamName = newTeamName;
                newTeam.TeamCount = 0;
                newTeam.Status = 1;
                await _context.AddAsync(newTeam);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveATeamAsync(int teamId)
        {
            var team = await _context.Teams.Where(t => t.TeamId == teamId).FirstOrDefaultAsync();
            var participant = await _context.Participants.Where(p => p.TeamId == teamId).ToListAsync();
            var teacherTeam = await _context.TeacherTeams.Where(tt => tt.TeamId == teamId).ToListAsync();
            var teamTopic = await _context.TeamTopics.Where(tp => tp.TeamId == teamId).ToListAsync();
            var courseTeam = await _context.CourseTeams.Where(ct => ct.TeamId == teamId).ToListAsync();
            if (participant != null)
            {
                foreach (var p in participant)
                {
                    p.TeamId = null;
                }
            }
            if (teacherTeam != null)
            {
                _context.TeacherTeams.RemoveRange(teacherTeam);
            }
            if (teamTopic != null)
            {
                _context.TeamTopics.RemoveRange(teamTopic);
            }
            if (courseTeam != null)
            {
                _context.CourseTeams.RemoveRange(courseTeam);
            }
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveAStudentInTeamAsync(int studentId, int teamId)
        {
            var participant = await _context.Participants.Where(p => p.StuId == studentId && p.TeamId == teamId).FirstOrDefaultAsync();
            var team = await _context.Teams.Where(t => t.TeamId == teamId).FirstOrDefaultAsync();
            if (participant == null || team == null)
            {
                return false;
            }
            team.TeamCount--;
            participant.TeamId = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateATeamAsync(TeamDTO teamDTO)
        {
            if (teamDTO != null)
            {
                var updateTeam = _mapper.Map<Team>(teamDTO);
                _context.Update(updateTeam);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddTopicToTeamAsync(AddTopicToTeam addTT)
        {
            var team = await _context.Teams.FindAsync(addTT.TeamId);
            if (team != null)
            {
                var teamTopic = await _context.TeamTopics.AnyAsync(tt => tt.TeamId == team.TeamId);
                if (!teamTopic)
                {
                    var isExistTopic = await _context.TeamTopics.Where(n => n.TopicId == addTT.TopicId).ToListAsync();
                    foreach (var t in isExistTopic)
                    {
                        if (t.TopicId == addTT.TopicId) return false;

                    }
                    var newTeamTopic = new TeamTopic
                    {
                        TeamId = team.TeamId,
                        TopicId = addTT.TopicId
                    };
                    var topic = await _context.Topics.FindAsync(addTT.TopicId);
                    var courseId = await _context.CourseTeams.Where(ct => ct.TeamId == team.TeamId).Select(c => c.CourseId).FirstOrDefaultAsync();
                    topic.CourseId = courseId;
                    _context.TeamTopics.Add(newTeamTopic);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var isExistTopic = await _context.TeamTopics.Where(n => n.TopicId == addTT.TopicId).ToListAsync();
                    foreach (var t in isExistTopic)
                    {
                        if (t.TopicId == addTT.TopicId) return false;

                    }
                    var topic = await _context.TeamTopics.Where(tt => tt.TeamId == team.TeamId).FirstOrDefaultAsync();
                    _context.TeamTopics.Remove(topic);
                    var newTeamTopic = new TeamTopic
                    {
                        TeamId = team.TeamId,
                        TopicId = addTT.TopicId
                    };
                    _context.TeamTopics.Add(newTeamTopic);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
