using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamManagement.DTO;
using TeamManagement.Repository.Models;
using TeamManagement.Repository.RequestBodyModel;
using TeamManagement.RequestBodyModel;

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

        public async Task<List<StudentDTO>> GetListStudentByTeamIdAsync(int teamId)
        {
            var listStudent = await _context.Participants.Where(t => t.TeamId == teamId).Select(s => s.Stu).ToListAsync();
            return _mapper.Map<List<StudentDTO>>(listStudent);
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
    }
}
